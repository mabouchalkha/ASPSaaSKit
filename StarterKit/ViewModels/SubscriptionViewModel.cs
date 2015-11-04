using StarterKit.Architecture.Interfaces;
using StarterKit.Architecture.Interfaces.Mapping;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.ViewModels
{
    public class SubscriptionViewModel : IMapFrom<StripeSubscription>, ICanMap
    {
        public DateTime? PeriodEnd { get; set; }
        public DateTime? PeriodStart { get; set; }
        public DateTime? TrialEnd { get; set; }
        public DateTime? TrialStart { get; set; }
        public string Status { get; set; }

        public decimal StripePlanAmount { get; set; }
        public string StripePlanName { get; set; }
        public string StripePlanInterval { get; set; }
        public int? StripePlanTrialPeriodDays { get; set; }
    }
}
