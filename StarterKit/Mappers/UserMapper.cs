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
                EmailConfirmed = user.EmailConfirmed
            };
        }

        public static DetailUserViewModel MapToDetailUserViewModel(this ApplicationUser user)
        {
            return new DetailUserViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Id = user.Id,
                TwoFactorEnabled = user.TwoFactorEnabled
            };
        }

        public static ApplicationUser MapToApplicationUser(this DetailUserViewModel viewModel, ApplicationUser user = null)
        {
            if (user != null)
            {
                if (user.Email.Equals(viewModel.Email, StringComparison.OrdinalIgnoreCase))
                {
                    user.EmailConfirmed = false;
                    user.UserName = viewModel.Email;
                }

                //send new confirm email
                user.LastName = viewModel.LastName;
                user.FirstName = viewModel.FirstName;
                user.Email = viewModel.Email;
            }
            else
            {
                user = new ApplicationUser();
                user.EmailConfirmed = false;
                user.LastName = viewModel.LastName;
                user.FirstName = viewModel.FirstName;
                user.Email = viewModel.Email;
                user.UserName = viewModel.Email;
                user.TwoFactorEnabled = viewModel.TwoFactorEnabled;
            }

            return user;
        }
    }
}