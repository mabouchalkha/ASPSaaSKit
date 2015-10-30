using StarterKit.Architecture.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarterKit.DOM
{
    public class Tenant : IIdentifiableEntity<Guid>
    {
        public Tenant() { }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public string StripeCustomerId { get; set; }
        public string StripeSubscriptionId { get; set; }
        public DateTime ActiveUntil { get; set; }
        public DateTime? CreditCardExpires { get; set; }
        public bool IsTrial { get; set; }

        public string OwnerEmail { get; set; } 

        public ICollection<ApplicationUser> ApplicationUser { get; set; }

        public Guid EntityId
        {
            get { return Id; }
            set { Id = value; }
        }
    }
}
