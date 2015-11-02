using FluentValidation;
using StarterKit.ViewModels;

namespace StarterKit.Fluent.ViewModels
{
    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            RuleFor(vm => vm.Password).NotEmpty().WithLocalizedMessage(() => App_GlobalResources.lang.firstNameRequired);
            RuleFor(vm => vm.Email).EmailAddress().NotEmpty().WithLocalizedMessage(() => App_GlobalResources.lang.emailRequired);
        }
    }

    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator()
        {
            //RuleFor(vm => vm.Password)
            //    .Length(2, 6).WithLocalizedMessage(() => App_GlobalResources.lang.passwordLength)
            //    .NotEmpty().WithLocalizedMessage(() => App_GlobalResources.lang.passwordRequired);
            //RuleFor(vm => vm.Email).EmailAddress().NotEmpty().WithLocalizedMessage(() => App_GlobalResources.lang.emailRequired);
            //RuleFor(vm => vm.FirstName).NotEmpty().WithLocalizedMessage(() => App_GlobalResources.lang.firstNameRequired);
            //RuleFor(vm => vm.LastName).NotEmpty().WithLocalizedMessage(() => App_GlobalResources.lang.lastNameRequired);
            //RuleFor(vm => vm.ConfirmPassword).Equal(x => x.Password).WithLocalizedMessage(() => App_GlobalResources.lang.passwordMatch);
        }
    }

    public class ConfirmEmailValidator : AbstractValidator<ConfirmEmail>
    {
        public ConfirmEmailValidator()
        {
            RuleFor(vm => vm.id).NotEmpty().WithLocalizedMessage(() => App_GlobalResources.lang.idRequired);
            RuleFor(vm => vm.Code).NotEmpty().WithLocalizedMessage(() => App_GlobalResources.lang.codeRequired);
        }
    }

    public class ForgotViewModelValidator : AbstractValidator<ForgotViewModel>
    {
        public ForgotViewModelValidator()
        {
            RuleFor(vm => vm.Email).EmailAddress().NotEmpty().WithLocalizedMessage(() => App_GlobalResources.lang.emailRequired);
        }
    }

    public class ResetPasswordViewModelValidator : AbstractValidator<ResetPasswordViewModel>
    {
        public ResetPasswordViewModelValidator()
        {
            RuleFor(vm => vm.Password)
                .Length(2, 6).WithLocalizedMessage(() => App_GlobalResources.lang.passwordLength)
                .NotEmpty().WithLocalizedMessage(() => App_GlobalResources.lang.passwordRequired);
            RuleFor(vm => vm.Id).NotEmpty().WithLocalizedMessage(() => App_GlobalResources.lang.idRequired);
            RuleFor(vm => vm.Code).NotEmpty().WithLocalizedMessage(() => App_GlobalResources.lang.codeRequired);
            RuleFor(vm => vm.ConfirmPassword).Equal(x => x.Password).WithLocalizedMessage(() => App_GlobalResources.lang.passwordMatch);
        }
    }
}