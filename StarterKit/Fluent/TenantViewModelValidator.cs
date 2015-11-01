using FluentValidation;
using StarterKit.ViewModels;

namespace StarterKit.Fluent.ViewModels
{
    public class TenantViewModelValidator : AbstractValidator<TenantViewModel>
    {
        public TenantViewModelValidator()
        {
            RuleFor(vm => vm.Id).NotEmpty().WithLocalizedMessage(() => App_GlobalResources.lang.idRequired);
            RuleFor(vm => vm.Name).NotEmpty().WithLocalizedMessage(() => App_GlobalResources.lang.lastNameRequired);
        }
    }
}