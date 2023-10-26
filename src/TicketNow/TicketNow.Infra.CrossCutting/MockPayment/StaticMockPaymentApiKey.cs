using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace TicketNow.Infra.CrossCutting.MockPayment
{
    public static class StaticMockPaymentApiKey
    {
        public static string ApiKey { get; set; } = GetapiKey();

        private static string responseBody;

        private static string GetapiKey()
        {
            var _configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                   .AddJsonFile(path: "AppSettings.json", optional: false, reloadOnChange: true).Build();

            var username = _configuration.GetSection("MockPayment:Username").Value.ToString();
            var password = _configuration.GetSection("MockPayment:Password").Value.ToString();
            var urlGetapikey = _configuration.GetSection("MockPayment:Urlbase").Value.ToString() + _configuration.GetSection("MockPayment:GetApiKey").Value.ToString();

            var getapikeyDto = new GetApiKey()
            {
                Username = username,
                Password = password
            };

            using (var httpClient = new HttpClient())
            {
                var httpContent = new StringContent(JsonSerializer.Serialize(getapikeyDto), System.Text.Encoding.UTF8, "application/json");
                var response = httpClient.PostAsync(urlGetapikey, httpContent).Result;

                if (!response.StatusCode.Equals(HttpStatusCode.OK))
                    throw new Exception(response.Content.ToString());

                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response.EnsureSuccessStatusCode();

                responseBody = response.Content.ReadFromJsonAsync<string>().Result.ToString();
            }

            return responseBody;
        }
    }
}