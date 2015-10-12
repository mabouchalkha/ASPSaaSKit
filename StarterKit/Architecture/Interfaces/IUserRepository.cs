using Microsoft.AspNet.Identity;
using StarterKit.DOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.Architecture.Interfaces
{
    public interface IUserRepository : IBaseRepository<ApplicationUser, string>
    {
        bool HasPendingChange(ApplicationUser entity);
        Task<IdentityResult> ValidateUser(ApplicationUser entity);
        bool EmailExit(string email);
    }
}
