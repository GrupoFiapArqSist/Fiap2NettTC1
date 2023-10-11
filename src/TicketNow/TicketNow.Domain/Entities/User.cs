﻿using Microsoft.AspNetCore.Identity;
using TicketNow.Domain.Enums;
using TicketNow.Domain.Interfaces.Entities;

namespace TicketNow.Domain.Entities
{
    public class User : IdentityUser<int>, IEntity<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }    
        public string Document { get; set; }
        public DocumentType DocumentType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool Active { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}