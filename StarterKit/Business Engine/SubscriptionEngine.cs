using StarterKit.Business_Engine.Interfaces;
using StarterKit.DOM;
using StarterKit.Repositories.Interfaces;
using Stripe;
using System;
using System.ComponentModel.Composition;
using System.Linq;

namespace StarterKit.Business_Engine
{
    [Export(typeof(ISubscriptionEngine))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SubscriptionEngine : ISubscriptionEngine
    {
        [ImportingConstructor]
        public SubscriptionEngine(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }

        private IDataRepositoryFactory _DataRepositoryFactory;

        private StripeCustomerService _stripeCustomerSerive;
        public StripeCustomerService StripeCustomerService
        {
            get
            {
                return _stripeCustomerSerive ?? new StripeCustomerService();
            }
            set
            {
                _stripeCustomerSerive = value;
            }
        }

        private StripeSubscriptionService _stripeSubscriptionService;
        public StripeSubscriptionService StripeSubscriptionService
        {
            get
            {
                return _stripeSubscriptionService ?? new StripeSubscriptionService();
            }
            private set
            {
                _stripeSubscriptionService = value;
            }
        }

        public StripeSubscription GetSubscriptionsTenant(Tenant tenant)
        {
            StripeSubscription subscription = null;

            if (!string.IsNullOrWhiteSpace(tenant.StripeCustomerId) && !string.IsNullOrWhiteSpace(tenant.StripeSubscriptionId))
            {
                subscription = StripeCustomerService.Get(tenant.StripeCustomerId).StripeSubscriptionList.Data.FirstOrDefault(s => s.Id == tenant.StripeSubscriptionId);
            }

            return subscription;
        }

        public Tenant SubscribeTenant (Tenant tenant, int planId, string stripeTokenId)
        {
            ISubscriptionPlanRepository subscriptionPlanRepository = _DataRepositoryFactory.GetDataRepository<ISubscriptionPlanRepository>();
            ITenantRepository tenantRepository = _DataRepositoryFactory.GetDataRepository<ITenantRepository>();

            try
            {
                SubscriptionPlan plan = subscriptionPlanRepository.Read(planId);
                
                if (string.IsNullOrWhiteSpace(tenant.StripeCustomerId))
                {
                    var customer = new StripeCustomerCreateOptions
                    {  
                        Email = tenant.OwnerEmail,
                        Source = string.IsNullOrWhiteSpace(stripeTokenId) ? null : new StripeSourceOptions() { TokenId = stripeTokenId },
                        PlanId = plan.ExternalId,
                    };

                    StripeCustomer stripeCustomer = StripeCustomerService.Create(customer);

                    tenant.StripeCustomerId = stripeCustomer.Id;
                    tenant.StripeSubscriptionId = stripeCustomer.StripeSubscriptionList.Data.SingleOrDefault().Id;
                    tenant.ActiveUntil = DateTime.Now.AddDays((double)plan.TrialPeriodDays);
                    tenantRepository.Update(tenant);
                }
                else
                {
                    var subscriptionOptions = new StripeSubscriptionUpdateOptions
                    {
                        PlanId = plan.ExternalId
                    };

                    StripeSubscription stripeSubscription =
                        StripeSubscriptionService.Update(tenant.StripeCustomerId, tenant.StripeSubscriptionId, subscriptionOptions);

                    tenant.ActiveUntil = stripeSubscription.PeriodEnd.Value;
                    tenantRepository.Update(tenant);
                }
            }
            catch (StripeException stripeException)
            {
                throw;
            }

            return tenant;
        }
    }
}
