using System.ComponentModel;

namespace TicketNow.Domain.Enums
{
    public enum OrderStatusEnum
    {
        [Description("Active")]
        Active = 0,

        [Description("Canceled")]
        Canceled = 1
    }
}
