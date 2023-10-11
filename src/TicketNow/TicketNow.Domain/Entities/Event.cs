﻿using TicketNow.Domain.Enums;
using TicketNow.Domain.Interfaces.Entities;

namespace TicketNow.Domain.Entities
{
    public class Event : BaseEntity, IEntity<int>
    {
        public int CategoryId { get; set; }
        public int PrometerId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string UF { get; set; }
        public string Description { get; set; }
        public string EventTime { get; set; }
        public DateTime EventDate { get; set; }
        public Category Category { get; set; }
        public decimal TicketPrice { get; set; }
        public long TicketAmount { get; set; }
        public long TicketAvailable { get; set; }
        public virtual User Promoter { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}