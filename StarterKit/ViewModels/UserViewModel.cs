using FluentValidation.Attributes;
using StarterKit.Architecture.Bases;
using StarterKit.DOM;
using StarterKit.Fluent.ViewModels;

namespace StarterKit.ViewModels
{
    public class IndexUserViewModel : BaseViewModel<IndexUserViewModel>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
        public bool EmailConfirmed { get; set; }
    }

    [Validator(typeof(DetailUserViewModelValidator))]
    public class DetailUserViewModel : BaseViewModel<DetailUserViewModel>
    {       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
        public bool TwoFactorEnabled { get; set; }
    }
}