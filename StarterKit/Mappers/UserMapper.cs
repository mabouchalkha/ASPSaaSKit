using StarterKit.DOM;
using StarterKit.Repositories.Interfaces;
using StarterKit.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StarterKit.Mappers
{
    public static class UserMapper
    {
        public static List<IndexUserViewModel> MapToViewModels(this List<ApplicationUser> users)
        {
            List<IndexUserViewModel> usersViewModel = new List<IndexUserViewModel>();

            if (users != null)
            {
                users.ForEach(u => usersViewModel.Add(u.MapToIndexViewModel()));
            }

            return usersViewModel;
        }

        public static IndexUserViewModel MapToIndexViewModel(this ApplicationUser user)
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

        public static DetailUserViewModel MapToViewModel(this ApplicationUser user)
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

        public static ApplicationUser MapFromViewModel(this DetailUserViewModel viewModel)
        {
            return new ApplicationUser()
            {
                EmailConfirmed = false,
                LastName = viewModel.LastName,
                FirstName = viewModel.FirstName,
                Email = viewModel.Email,
                UserName = viewModel.Email,
                TwoFactorEnabled = viewModel.TwoFactorEnabled,
                Id = viewModel.Id
            };
        }
    }
}