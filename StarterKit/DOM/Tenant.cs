﻿using FluentValidation.Attributes;
using StarterKit.Architecture.Interfaces;
using StarterKit.Fluent.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarterKit.DOM
{
    public class Tenant : IIdentifiableTenantableEntity<Guid>
    {
        public Guid TenantId { get; set; }
        public string Name { get; set; }

        public string StripeCustomerId { get; set; }
        public DateTime ActiveUntil { get; set; }
        public DateTime? CreditCardExpires { get; set; }
        public bool IsTrial { get; set; }
        
        public Guid EntityId
        {
            get { return TenantId; }
            set { TenantId = value; }
        }
    }
}
