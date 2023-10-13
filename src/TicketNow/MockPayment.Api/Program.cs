using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MockPayment.Api;
using MockPayment.Api.Background;
using MockPayment.Api.Model;
using MockPayment.Api.Model.Dto.Application;
using MockPayment.Api.Model.Dto.Payment;
using MockPayment.Api.Model.Enums;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MockPayment", Description = "Mock Payment Api", Version = "v1" });
    c.OperationFilter<AuthHeaderFilter>();
});

#region [DB]

builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("local");
    options.UseSqlServer(connectionString);
});

#endregion
builder.Services.AddHostedService<Engine>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "Mock Payment Api"));
}

#region [Migrations and Seeds]
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider
        .GetRequiredService<ApplicationDbContext>();

    dbContext.Database.Migrate();
}
#endregion

app.UseHttpsRedirection();

#region [Endpoints]

app.MapGet("/application", async (ApplicationDbContext _context, HttpRequest request) =>
{
    var apiKey = request.Headers["API-KEY"];
    if (string.IsNullOrWhiteSpace(apiKey))
        return Results.NotFound("Chave invalida!");

    var application = await _context.Applications.FirstOrDefaultAsync(app => app.ApiKey == apiKey);
    if (application is null)
        return Results.NotFound("Aplicação não encontrada!");

    return Results.Ok(application);
});

app.MapPost("/application", async (ApplicationDbContext _context, CreateApplicationDto applicationDto) =>
{
    if (string.IsNullOrWhiteSpace(applicationDto.Username) ||
    string.IsNullOrWhiteSpace(applicationDto.Password) ||
    string.IsNullOrWhiteSpace(applicationDto.WebhookUrl))
        return Results.BadRequest("Requisição invalida!");

    if (_context.Applications.Any(app => app.Username == applicationDto.Username))
        return Results.BadRequest("Aplicação já cadastrada!");

    var entity = new Application(applicationDto.Username,
                                 applicationDto.Password,
                                 applicationDto.WebhookUrl);

    entity.ApiKey = Guid.NewGuid().ToString().Split("-")[0];

    _context.Applications.Add(entity);
    await _context.SaveChangesAsync();

    return Results.Ok(entity);
});

app.MapPost("/application/get-api-key", async (ApplicationDbContext _context, GetApiKeyDto getApiKeyRequestDto) =>
{
    if (string.IsNullOrWhiteSpace(getApiKeyRequestDto.Username) ||
        string.IsNullOrWhiteSpace(getApiKeyRequestDto.Password))
        return Results.BadRequest("Requisição invalida!");

    var application = await _context.Applications.FirstOrDefaultAsync(app => app.Username == getApiKeyRequestDto.Username &&
                                                                             app.Password == getApiKeyRequestDto.Password);
    if (application is null)
        return Results.NotFound("Aplicação não encontrada!");

    application.ApiKey = Guid.NewGuid().ToString().Split("-")[0];
    _context.Applications.Update(application);
    await _context.SaveChangesAsync();

    return Results.Ok(application.ApiKey);
});

app.MapPost("/payment", async (ApplicationDbContext _context, HttpRequest request, CreatePaymentDto createPaymentDto) =>
{
    var apiKey = request.Headers["API-KEY"];

    if (string.IsNullOrWhiteSpace(apiKey.ToString()) ||
        createPaymentDto.OrderId == 0 ||
        createPaymentDto.PaymentMethod == 0)
        return Results.BadRequest("Requisição invalida!");

    var application = await _context.Applications.FirstOrDefaultAsync(app => app.ApiKey == apiKey.ToString());
    if (application is null)
        return Results.NotFound("Aplicação não encontrada!");

    if (_context.Payments.Any(p => p.OrderId == createPaymentDto.OrderId &&
                                   p.ApplicationId == application.Id))
        return Results.BadRequest("Pedido já processado!");

    var paymentStatus = createPaymentDto.PaymentMethod == PaymentMethod.CreditCard ?
                        PaymentStatus.Paid :
                        PaymentStatus.WaitingPayment;

    if (paymentStatus == PaymentStatus.Paid)
    {
        Random random = new Random();
        var chance = random.NextDouble();
        if (chance < 0.5)
            paymentStatus = PaymentStatus.Unauthorized;
    }
    var entity = new Payment(application.Id,
                             createPaymentDto.OrderId,
                             createPaymentDto.PaymentMethod,
                             paymentStatus);

    _context.Payments.Add(entity);
    await _context.SaveChangesAsync();

    return Results.Ok(entity);
});

app.MapGet("/payment/get-payment-by-order/{orderId}", async (ApplicationDbContext _context, HttpRequest request, int orderId) =>
{
    var apiKey = request.Headers["API-KEY"];

    if (string.IsNullOrWhiteSpace(apiKey.ToString()) ||
        orderId == 0)
        return Results.BadRequest("Requisição invalida!");

    var application = await _context.Applications.FirstOrDefaultAsync(a => a.ApiKey == apiKey.ToString());
    var payment = await _context.Payments
                           .FirstOrDefaultAsync(p => p.OrderId == orderId);

    if (payment is null ||
        application is null ||
        payment.ApplicationId != application.Id)
        return Results.BadRequest("Pagamento não encontrado!");

    return await Task.FromResult(Results.Ok(payment));
});
#endregion

app.Run();