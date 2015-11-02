using FluentValidation.Attributes;
using StarterKit.Fluent.ViewModels;
using System;

namespace StarterKit.ViewModels
{
    [Validator(typeof(TenantViewModelValidator))]
    public class RoleVieWModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}