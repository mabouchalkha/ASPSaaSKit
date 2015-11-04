using FluentValidation.Attributes;
using StarterKit.Architecture.Interfaces;
using StarterKit.Fluent.ViewModels;
using System;

namespace StarterKit.ViewModels
{
    [Validator(typeof(TenantViewModelValidator))]
    public class TenantViewModel : ICanMap
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsTrial { get; set; }
    }
}