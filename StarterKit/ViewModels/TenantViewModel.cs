using FluentValidation.Attributes;
using StarterKit.Fluent.ViewModels;
using System;

namespace StarterKit.ViewModels
{
    [Validator(typeof(TenantViewModelValidator))]
    public class TenantViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsTrial { get; set; }
    }
}