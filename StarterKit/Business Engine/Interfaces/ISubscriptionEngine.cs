using StarterKit.Architecture.Interfaces;
using StarterKit.DOM;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.Business_Engine.Interfaces
{
    public interface ISubscriptionEngine : IBusinessEngine
    {
        StripeSubscription GetSubscriptionsTenant(Tenant tenant);
        Tenant SubscribeTenant(Tenant tenant, int planId, string stripeTokenId);
    }
}
