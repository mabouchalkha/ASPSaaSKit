﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.ViewModels
{
    public class BillingViewModel
    {
        public int SubscriptionPlanId { get; set; }
        public string StripeTokenId { get; set; }
    }
}
