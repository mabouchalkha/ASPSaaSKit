using FluentValidation;
using StarterKit.ViewModels;

namespace StarterKit.Fluent.ViewModels
{
    public class DetailUserViewModelValidator : AbstractValidator<DetailUserViewModel>
    {
        public DetailUserViewModelValidator()
        {
            RuleFor(vm => vm.FirstName).NotEmpty().WithLocalizedMessage(() => App_GlobalResources.lang.firstNameRequired);
            RuleFor(vm => vm.LastName).NotEmpty().WithLocalizedMessage(() => App_GlobalResources.lang.lastNameRequired);
            RuleFor(vm => vm.Email).EmailAddress().NotEmpty().WithLocalizedMessage(() => App_GlobalResources.lang.emailRequired);
        }
    }
}