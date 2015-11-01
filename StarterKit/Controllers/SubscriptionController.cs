using Microsoft.AspNet.Identity;
using StarterKit.Architecture.Bases;
using StarterKit.Business_Engine.Interfaces;
using StarterKit.Repositories.Interfaces;
using StarterKit.ViewModels;
using System.ComponentModel.Composition;
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
            IUserRepository userRepository = _DataRepositoryFactory.GetDataRepository<IUserRepository>();
            ISubscriptionEngine subscriptionEngine = _BusinessEngineFactory.GetBusinessEngine<ISubscriptionEngine>();

            SubscriptionViewModel subscription = null;
            var user = userRepository.Read(User.Identity.GetUserId(), u => u.Tenant);
            if (user != null)
            {
                subscription = AutoMapper.Mapper.Map<SubscriptionViewModel>(subscriptionEngine.GetSubscriptionsTenant(user.Tenant));
            }

            return success(string.Empty, subscription);
        }

        [HttpPost]
        public JsonResult Billing(BillingViewModel billingViewModel)
        {
            IUserRepository userRepository = _DataRepositoryFactory.GetDataRepository<IUserRepository>();
            ISubscriptionEngine subscriptionEngine = _BusinessEngineFactory.GetBusinessEngine<ISubscriptionEngine>();

            var user = userRepository.Read(User.Identity.GetUserId(), u => u.Tenant);
            if (user != null)
            {
                subscriptionEngine.SubscribeTenant(user.Tenant, billingViewModel.SubscriptionPlanId, billingViewModel.StripeTokenId);
            }

            return success(string.Empty);
        }
    }
}

