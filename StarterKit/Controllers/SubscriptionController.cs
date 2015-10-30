using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using StarterKit.Architecture.Bases;
using StarterKit.DOM;
using StarterKit.Repositories.Interfaces;
using Stripe;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace StarterKit.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SubscriptionController : BaseController
    {
        [ImportingConstructor]
        public SubscriptionController(ISubscriptionPlanRepository subscriptionPlanRepository,
                                      ISubscriptionRepository subscriptionRepository,
                                      IUserRepository userRepository)
        {
            _UserRepository = userRepository;
            _SubscriptionPlanRepository = subscriptionPlanRepository;
            _SubscriptionRepository = subscriptionRepository;
        }

        private IUserRepository _UserRepository;
        private ISubscriptionRepository _SubscriptionRepository;
        private ISubscriptionPlanRepository _SubscriptionPlanRepository;

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

        [HttpGet]
        public JsonResult Index()
        {
            return success(string.Empty, new { });
        }

        [HttpPost]
        public async Task<JsonResult> Billing(SubscriptionPlan subscriptionPlan, string userId, string stripeTokenId)
        {
            SubscriptionPlan plan = _SubscriptionPlanRepository.Read(subscriptionPlan.Id);
            try
            {
                // I need username with Tenant, Plan, StripeToken
                // ApplicationUserManager UserManager
                // StripeCustomerService to create customer
                // StripeSubscriptionService to create a subscription
                
                var user = await _UserRepository.UserManager.FindByNameAsync(User.Identity.Name);

                if (String.IsNullOrWhiteSpace(user.Tenant.StripeCustomerId))
                {
                    // create a customer which will create subscription if plan is set and cc info via stripetoken
                    var customer = new StripeCustomerCreateOptions
                    {
                        Email = user.Email,
                        Source = new StripeSourceOptions() { TokenId = stripeTokenId },
                        PlanId = plan.ExternalId
                    };

                    StripeCustomer stripeCustomer = StripeCustomerService.Create(customer);

                    // update user tenant
                    // store stripeCustomer.id so you can add or/and update a subscriptions on the same user tenant
                    // set activeuntildate

                    user.Tenant.StripeCustomerId = stripeCustomer.Id;
                    user.Tenant.ActiveUntil = DateTime.Now.AddDays((double)plan.TrialPeriodDays);
                    _UserRepository.Update(user);
                }
                else
                {
                    // customer tenant already exists, add subscription to customer
                    // update user
                    // set activeuntildate

                    StripeSubscription stripeSubscription = StripeSubscriptionService.Create(user.Tenant.StripeCustomerId, plan.ExternalId);
                    user.Tenant.ActiveUntil = DateTime.Now.AddDays((double)plan.TrialPeriodDays);
                    _UserRepository.Update(user);
                }
                //_SubscriptionRepository.Create()
            }
            catch (StripeException stripeException)
            {
                //ModelState.AddModelError("", stripeException);
            }
            return success(string.Empty, new { });
        }
    }
}

