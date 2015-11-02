using StarterKit.DOM;
using StarterKit.Repositories.Interfaces;
using StarterKit.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StarterKit.Mappers
{
    public static class RoleMapper
    {
        public static RoleVieWModel MapToViewModel(this ApplicationRole role)
        {
            return new RoleVieWModel()
            {
                Id = role.Id,
                Name = role.Name
            };
        }

        public static List<RoleVieWModel> MapToViewModel(this List<ApplicationRole> roles)
        {
            List<RoleVieWModel> roleViewModels = new List<RoleVieWModel>();

            if (roles != null)
            {
                foreach (ApplicationRole role in roles)
                {
                    roleViewModels.Add(RoleMapper.MapToViewModel(role));
                }
            }

            return roleViewModels;
        }
    }
}