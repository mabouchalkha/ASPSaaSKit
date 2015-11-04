using FluentValidation.Attributes;
using StarterKit.Fluent.ViewModels;
using System;

namespace StarterKit.ViewModels
{
    [Validator(typeof(TenantViewModelValidator))]
    public class RoleViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}