using TicketNow.Domain.Interfaces.Entities;

namespace TicketNow.Domain.Entities
{
    public class Application : BaseEntity, IEntity<int>
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string WebhookUrl { get; set; }
        public string ApiKey { get; set; }
    }
}
