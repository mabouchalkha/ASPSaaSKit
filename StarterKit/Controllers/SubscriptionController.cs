using Microsoft.AspNet.Identity;
using StarterKit.Architecture.Bases;
using StarterKit.Business_Engine;
using StarterKit.Business_Engine.Interfaces;
using StarterKit.DOM;
using StarterKit.Repositories.Interfaces;
using StarterKit.ViewModels;
using Stripe;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Web.Mvc;

namespace StarterKit.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SubscriptionController : BaseController
    {
        [ImportingConstructor]
        public SubscriptionController(IBusinessEngineFactory businessEngineFactory,
                                       IDataRepositoryFactory dataRepositoryFactory)
        {
            _BusinessEngineFactory = businessEngineFactory;
            _DataRepositoryFactory = dataRepositoryFactory;
        }

        private IBusinessEngineFactory _BusinessEngineFactory;
        private IDataRepositoryFactory _DataRepositoryFactory;

        [HttpGet]
        public JsonResult Index()
        {
            // May this data will be created by webhooks
            //IEnumerable<Subscription> subscriptions = _SubscriptionRepository.Index();
            // TODO Ensure if the subscription still valid in stripe
            // Maybe we need a mapper for view model
            IUserRepository userRepository = _DataRepositoryFactory.GetDataRepository<IUserRepository>();
            ISubscriptionEngine subscriptionEngine = _BusinessEngineFactory.GetBusinessEngine<ISubscriptionEngine>();

            var user = userRepository.Read(User.Identity.GetUserId(), u => u.Tenant);
            var subscription = subscriptionEngine.GetSubscriptionsTenant(user.Tenant);
            return success(string.Empty, subscription);
        }

        [HttpPost]
        public JsonResult Billing(SubscriptionViewModel subscriptionViewModel)
        {
            IUserRepository userRepository = _DataRepositoryFactory.GetDataRepository<IUserRepository>();
            ISubscriptionEngine subscriptionEngine = _BusinessEngineFactory.GetBusinessEngine<ISubscriptionEngine>();

            var user = userRepository.Read(User.Identity.GetUserId(), u => u.Tenant);
            var tenant = subscriptionEngine.SubscribeTenant(user.Tenant, subscriptionViewModel.SubscriptionPlanId, subscriptionViewModel.StripeTokenId);
            return success(string.Empty, tenant);
        }
    }
}

