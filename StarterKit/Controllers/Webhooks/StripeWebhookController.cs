using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Stripe;
using StarterKit.Repositories.Interfaces;
using System.ComponentModel.Composition;
using StarterKit.DOM;
using StarterKit.Architecture.Bases;

namespace StarterKit.Controllers.Webhooks
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class StripeWebhookController : BaseController
    {

        [ImportingConstructor]
        public StripeWebhookController(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }

        private IDataRepositoryFactory _DataRepositoryFactory;
        private IStripeEventLogRepository _StripeEventLogRepository;

        [HttpPost]
        public ActionResult Index()
        {
            _StripeEventLogRepository = _DataRepositoryFactory.GetDataRepository<IStripeEventLogRepository>();

            Stream request = Request.InputStream;
            request.Seek(0, SeekOrigin.Begin);
            string json = new StreamReader(request).ReadToEnd();

            StripeEvent stripeEvent = null;
            try
            {
                stripeEvent = StripeEventUtility.ParseEvent(json);
                stripeEvent = VerifyEventSentFromStripe(stripeEvent);
                if (HasEventBeenProcessedPreviously(stripeEvent))
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest,
                                string.Format("Unable to parse incoming event. The following error occured: {0}", ex.Message));
            }

            if (stripeEvent == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Incoming event empty");

            // var emailService = new EmailService();
            switch (stripeEvent.Type)
            {
                case StripeEvents.PlanCreated:
                    StripePlan planStripe = Mapper<StripePlan>.MapFromJson(stripeEvent.Data.Object.ToString());
                    SubscriptionPlan subscriptionPlan = new SubscriptionPlan
                    {
                        AmountInCents = planStripe.Amount,
                        Currency = planStripe.Currency,
                        Description = planStripe.StatementDescriptor,
                        DisplayOrder = 1,
                        ExternalId = planStripe.Id,
                        Interval = planStripe.Interval,
                        IsActive = true,
                        Name = planStripe.Name,
                        TrialPeriodDays = planStripe.TrialPeriodDays
                    };

                    ISubscriptionPlanRepository subscriptionPlanRepository = _DataRepositoryFactory.GetDataRepository<ISubscriptionPlanRepository>();
                    subscriptionPlanRepository.Create(subscriptionPlan);
                    break;
                case StripeEvents.PlanUpdated:
                    planStripe = Mapper<StripePlan>.MapFromJson(stripeEvent.Data.Object.ToString());

                    subscriptionPlanRepository = _DataRepositoryFactory.GetDataRepository<ISubscriptionPlanRepository>();
                    SubscriptionPlan plan = subscriptionPlanRepository.FindBy(sp => sp.ExternalId == planStripe.Id);
                    if (plan != null)
                    {
                        plan.AmountInCents = planStripe.Amount;
                        plan.Currency = planStripe.Currency;
                        plan.Description = planStripe.StatementDescriptor;
                        plan.DisplayOrder = 1;
                        plan.ExternalId = planStripe.Id;
                        plan.Interval = planStripe.Interval;
                        plan.IsActive = true;
                        plan.Name = planStripe.Name;
                        plan.TrialPeriodDays = planStripe.TrialPeriodDays;

                        subscriptionPlanRepository.Update(plan);
                    }
                    break;
                case StripeEvents.PlanDeleted:
                    planStripe = Mapper<StripePlan>.MapFromJson(stripeEvent.Data.Object.ToString());
                    subscriptionPlanRepository = _DataRepositoryFactory.GetDataRepository<ISubscriptionPlanRepository>();
                    subscriptionPlanRepository.DeleteBy(sp => sp.ExternalId == planStripe.Id);
                    break;
                case StripeEvents.ChargeRefunded:
                    var charge = Mapper<StripeCharge>.MapFromJson(stripeEvent.Data.Object.ToString());
                    // emailService.SendEmail(charge);
                    break;
                default:
                    break;
            }

            // Log Stripe eventId to StripeEvent table in application database
            _StripeEventLogRepository.Create(
                new StripeEventLog()
                {
                    stripeEventId = stripeEvent.Id,
                    Request = stripeEvent.Request,
                    Type = stripeEvent.Type,
                    LiveMode = stripeEvent.LiveMode,
                    UserId = stripeEvent.UserId
                });

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        private bool HasEventBeenProcessedPreviously(StripeEvent stripeEvent)
        {
            // Lookup in table StripeEvent log  by eventId
            // if eventid exists return true otherwise return false
            bool eventProcessed = false;
            StripeEventLog stripeEventLog = _StripeEventLogRepository.FindBy(sev => sev.stripeEventId == stripeEvent.Id);
            if (stripeEventLog != null)
            {
                eventProcessed = stripeEventLog.stripeEventId == stripeEvent.Id;
            }

            return eventProcessed;
        }

        private static StripeEvent VerifyEventSentFromStripe(StripeEvent stripeEvent)
        {
            var eventService = new StripeEventService();
            stripeEvent = eventService.Get(stripeEvent.Id);
            return stripeEvent;
        }
    }
}