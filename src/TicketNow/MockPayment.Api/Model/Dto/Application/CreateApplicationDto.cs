namespace MockPayment.Api.Model.Dto.Application
{
    public class CreateApplicationDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string WebhookUrl { get; set; }
    }
}
