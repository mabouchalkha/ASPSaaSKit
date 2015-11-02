using FluentValidation.Attributes;
using StarterKit.Architecture.Interfaces;
using StarterKit.Fluent.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarterKit.DOM
{
    public class Tenant : IIdentifiableEntity<Guid>, IModificationHistory
    {
        public Guid TenantId { get; set; }
        public string Name { get; set; }

        public string StripeCustomerId { get; set; }
        public string StripeSubscriptionId { get; set; }
        public DateTime ActiveUntil { get; set; }
        public DateTime? CreditCardExpires { get; set; }
        public bool IsTrial { get; set; }

        public string OwnerEmail { get; set; }
        public string OwnerId { get; set; }

        public ICollection<ApplicationUser> ApplicationUser { get; set; }

        public Guid EntityId
        {
            get { return TenantId; }
            set { TenantId = value; }
        }

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
