using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using TicketNow.Domain.Dtos.MockPayment;
using TicketNow.Domain.Entities;
using TicketNow.Infra.Data.Context;

namespace TicketNow.Infra.Data.Seeds._SeedHistory
{
    public class Seed_20231016160000_Add_Application_Mock_Api : Seed
    {
        private readonly ApplicationDbContext _dbContext;
        private IConfiguration _configuration;

        public Seed_20231016160000_Add_Application_Mock_Api(ApplicationDbContext dbContext,
            IServiceProvider serviceProvider) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Up()
        {
            _configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                   .AddJsonFile(path: "AppSettings.json", optional: false, reloadOnChange: true).Build();

            var username = _configuration.GetSection("MockPayment:Username").Value.ToString();
            var password = _configuration.GetSection("MockPayment:Password").Value.ToString();
            var urlApplication = _configuration.GetSection("MockPayment:Urlbase").Value.ToString() + _configuration.GetSection("MockPayment:Application").Value.ToString();
            var urltickenow = _configuration.GetSection("Ticketnow:UrlBase").Value.ToString();

            using (var httpClient = new HttpClient())
            {
                var createApplication = new CreateApplicationDto()
                {
                    Username = username,
                    Password = password,
                    WebhookUrl = urltickenow
                };

                var httpContent = new StringContent(JsonSerializer.Serialize(createApplication), System.Text.Encoding.UTF8, "application/json");

                var response = httpClient.PostAsync(urlApplication, httpContent).Result;

                if (response.StatusCode.Equals(HttpStatusCode.BadRequest))
                    throw new Exception(response.Content.ToString());
            }
        }
    }
}
