using StarterKit.Architecture.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarterKit.DOM
{
    public class SubscriptionPlan : IIdentifiableEntity<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int AmountInCents { get; set; }
        public string Currency { get; set; }
        public string Interval { get; set; }
        public int? TrialPeriodDays { get; set; }

        public string ExternalId { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int DisplayOrder { get; set; }
        public string CSSClass { get; set; }
        public ICollection<Feature> Feature { get; set; }

        public SubscriptionPlan()
        {
            Feature = new List<Feature>();
        }

        public int AmountInDollars
        {
            get
            {
                return (int)Math.Floor((decimal)this.AmountInCents / 100);
            }
        }

        public int EntityId
        {
            get { return Id; }
            set { Id = value; }
        }
    }
}
