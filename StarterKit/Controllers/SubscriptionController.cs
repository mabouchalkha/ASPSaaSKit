using Microsoft.AspNet.Identity;
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

                var userManager = await _UserRepository.GetCurrentUser().FindByIdAsync(userId);

                if (String.IsNullOrWhiteSpace(userManager.Tenant.StripeCustomerId))
                {
                    // create a customer which will create subscription if plan is set and cc info via stripetoken
                    var customer = new StripeCustomerCreateOptions
                    {
                        Email = userManager.Email,
                        Source = new StripeSourceOptions() { TokenId = stripeTokenId},
                        PlanId = plan.ExternalId
                    };

                    StripeCustomerService stripeCustomerService = new StripeCustomerService();
                    StripeCustomer stripeCustomer = stripeCustomerService.Create(customer);

                    // update user tenant
                    // store stripeCustomer.id so you can add or/and update a subscriptions on the same user tenant
                    // set activeuntildate
                }
                else
                {
                    // customer tenant already exists, add subscription to customer
                    // update user
                    // set activeuntildate
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

