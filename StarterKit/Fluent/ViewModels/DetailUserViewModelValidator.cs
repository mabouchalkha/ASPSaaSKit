using FluentValidation;
using StarterKit.ViewModels;

namespace StarterKit.Fluent.ViewModels
{
    public class DetailUserViewModelValidator : AbstractValidator<DetailUserViewModel>
    {
        public DetailUserViewModelValidator()
        {
            //RuleFor(vm => vm.FirstName).NotEmpty().WithLocalizedMessage(() => Resources.lang.firstNameRequired);
            //RuleFor(vm => vm.LastName).NotEmpty().WithLocalizedMessage(() => Resources.lang.lastNameRequired);
            //RuleFor(vm => vm.Email).EmailAddress().NotEmpty().WithLocalizedMessage(() => Resources.lang.emailRequired);
        }
    }
}