using StarterKit.DOM;
using StarterKit.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StarterKit.Mappers
{
    public static class UserMapper
    {
        public static List<IndexUserViewModel> MapToIndexUserViewModels(this List<ApplicationUser> users)
        {
            List<IndexUserViewModel> usersViewModel = new List<IndexUserViewModel>();

            if (users != null)
            {
                users.ForEach(u => usersViewModel.Add(u.MapToIndexUserViewModel()));
            }

            return usersViewModel;
        }

        public static IndexUserViewModel MapToIndexUserViewModel(this ApplicationUser user)
        {
            return new IndexUserViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Id = user.Id,
            };
        }

        public static ApplicationUser MapToApplicationUser(this IndexUserViewModel viewModel, ApplicationUser user)
        {
            if (user.Email.Equals(viewModel.Email, StringComparison.OrdinalIgnoreCase))
            {
                user.EmailConfirmed = false;
            }

            // send confirm email to new email

            user.LastName = viewModel.LastName;
            user.FirstName = viewModel.FirstName;
            user.Email = viewModel.Email;

            return user;
        }
    }
}