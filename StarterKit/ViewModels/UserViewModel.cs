using FluentValidation.Attributes;
using StarterKit.Architecture.Interfaces;
using StarterKit.Fluent.ViewModels;

namespace StarterKit.ViewModels
{
    public class IndexUserViewModel : ICanMap
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
        public bool EmailConfirmed { get; set; }
    }

    [Validator(typeof(DetailUserViewModelValidator))]
    public class DetailUserViewModel : ICanMap
    {       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
        public bool TwoFactorEnabled { get; set; }
    }
}