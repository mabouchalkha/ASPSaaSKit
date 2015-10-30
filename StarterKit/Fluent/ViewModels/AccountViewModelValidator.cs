using FluentValidation;
using StarterKit.ViewModels;

namespace StarterKit.Fluent.ViewModels
{
    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            //RuleFor(vm => vm.Password).NotEmpty().WithLocalizedMessage(() => Resources.lang.firstNameRequired);
            //RuleFor(vm => vm.Email).EmailAddress().NotEmpty().WithLocalizedMessage(() => Resources.lang.emailRequired);
        }
    }

    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator()
        {
            //RuleFor(vm => vm.Password)
            //    .Length(2, 6).WithLocalizedMessage(() => Resources.lang.passwordLength)
            //    .NotEmpty().WithLocalizedMessage(() => Resources.lang.passwordRequired);
            //RuleFor(vm => vm.Email).EmailAddress().NotEmpty().WithLocalizedMessage(() => Resources.lang.emailRequired);
            //RuleFor(vm => vm.FirstName).NotEmpty().WithLocalizedMessage(() => Resources.lang.firstNameRequired);
            //RuleFor(vm => vm.LastName).NotEmpty().WithLocalizedMessage(() => Resources.lang.lastNameRequired);
            //RuleFor(vm => vm.ConfirmPassword).Equal(x => x.Password).WithLocalizedMessage(() => Resources.lang.passwordMatch);
        }
    }

    public class ConfirmEmailValidator : AbstractValidator<ConfirmEmail>
    {
        public ConfirmEmailValidator()
        {
            //RuleFor(vm => vm.id).NotEmpty().WithLocalizedMessage(() => Resources.lang.idRequired);
            //RuleFor(vm => vm.Code).NotEmpty().WithLocalizedMessage(() => Resources.lang.codeRequired);
        }
    }

    public class ForgotViewModelValidator : AbstractValidator<ForgotViewModel>
    {
        public ForgotViewModelValidator()
        {
            //RuleFor(vm => vm.Email).EmailAddress().NotEmpty().WithLocalizedMessage(() => Resources.lang.emailRequired);
        }
    }

    public class ResetPasswordViewModelValidator : AbstractValidator<ResetPasswordViewModel>
    {
        public ResetPasswordViewModelValidator()
        {
            //RuleFor(vm => vm.Password)
            //    .Length(2, 6).WithLocalizedMessage(() => Resources.lang.passwordLength)
            //    .NotEmpty().WithLocalizedMessage(() => Resources.lang.passwordRequired);
            //RuleFor(vm => vm.Id).NotEmpty().WithLocalizedMessage(() => Resources.lang.idRequired);
            //RuleFor(vm => vm.Code).NotEmpty().WithLocalizedMessage(() => Resources.lang.codeRequired);
            //RuleFor(vm => vm.ConfirmPassword).Equal(x => x.Password).WithLocalizedMessage(() => Resources.lang.passwordMatch);
        }
    }
}