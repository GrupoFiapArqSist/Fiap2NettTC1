using Microsoft.EntityFrameworkCore;
using MockPayment.Api.Model;
using MockPayment.Api.Model.Enums;
using System.Diagnostics;
using System.Text.Json;

namespace MockPayment.Api.Background
{
    public class Engine : BackgroundService, IHostedService
    {        
        private readonly IDbContextFactory<ApplicationDbContext> _context;
        private readonly TimeSpan _period = TimeSpan.FromMinutes(2);

        public Engine(IDbContextFactory<ApplicationDbContext> context)
        {
            _context = context;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using PeriodicTimer timer = new PeriodicTimer(_period);
            while (
                !stoppingToken.IsCancellationRequested &&
                await timer.WaitForNextTickAsync(stoppingToken))
            {
                using (ApplicationDbContext dbContext = _context.CreateDbContext())
                {
                    await ProcessPayments(dbContext);
                }
            }
        }

        private async Task ProcessPayments(ApplicationDbContext _context)
        {
            Debug.WriteLine("Executando processamento");

            var paymentsToByNotified = new List<Payment>();
            var paymentsToProcess = _context.Payments
                .Include(x => x.Application)
                .Where(p => p.PaymentStatus == PaymentStatus.WaitingPayment)
                .OrderBy(p => p.CreatedAt)
                .ToList();

            foreach (var payment in paymentsToProcess)
            {
                Random random = new Random();
                var chance = random.NextDouble();
                if (chance < 0.5)
                    payment.PaymentStatus = PaymentStatus.Paid;
                else
                    payment.PaymentStatus = PaymentStatus.Expired;

                _context.Payments.Update(payment);
                paymentsToByNotified.Add(payment);
            }

            await _context.SaveChangesAsync();

            await NotifyPayments(paymentsToByNotified);
        }

        private async Task NotifyPayments(List<Payment> payments)
        {
            using (var httpClient = new HttpClient())
            {
                foreach (var payment in payments)
                {
                    try
                    {
                        var httpContent = new StringContent(JsonSerializer.Serialize(payments),
                            System.Text.Encoding.UTF8, "application/json");

                        var response = await httpClient.PostAsync(payment.Application?.WebhookUrl, httpContent);

                        if (!response.IsSuccessStatusCode)
                            Debug.WriteLine($"Erro ao enviar a requisição. Código de status: {response.StatusCode}");
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Erro: {ex.Message}");
                    }
                }
            }
        }
    }
}
