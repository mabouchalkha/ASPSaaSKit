using FluentValidation.Attributes;
using Microsoft.AspNet.Identity.Owin;
using StarterKit.Fluent.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StarterKit.ViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    [Validator(typeof(ForgotViewModelValidator))]
    public class ForgotViewModel
    {
        public string Email { get; set; }
    }

    [Validator(typeof(LoginViewModelValidator))]
    public class LoginViewModel
    {        
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }

    [Validator(typeof(RegisterViewModelValidator))]
    public class RegisterViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    [Validator(typeof(ConfirmEmailValidator))]
    public class ConfirmEmail
    {
        public string id { get; set; }
        public string Code { get; set; }
    }
    
    [Validator(typeof(ResetPasswordViewModelValidator))]
    public class ResetPasswordViewModel
    {
        public string Id { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
