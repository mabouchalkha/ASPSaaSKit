using StarterKit.Architecture.Bases;
using StarterKit.DOM;
using StarterKit.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StarterKit.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SubscriptionPlanController : BaseController
    {
        private ISubscriptionPlanRepository _subscriptionPlanRepository;

        [ImportingConstructor]
        public SubscriptionPlanController(ISubscriptionPlanRepository subscriptionPlanRepository)
        {
            _subscriptionPlanRepository = subscriptionPlanRepository;
        }

        // GET: SubscriptionPlan
        [HttpGet]
        public JsonResult Index()
        {
            IEnumerable<SubscriptionPlan> plans = _subscriptionPlanRepository.Index(sp => sp.Feature);
            return success(string.Empty, plans);
        }


    }
}