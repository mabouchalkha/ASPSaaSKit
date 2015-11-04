using StarterKit.Architecture.Bases;
using StarterKit.Authorize;
using StarterKit.DOM;
using StarterKit.Helpers;
using StarterKit.Mappers;
using StarterKit.Repositories.Interfaces;
using StarterKit.Utils;
using StarterKit.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace StarterKit.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Authorize]
    [AuthorizeSubscriber]
    public class UserController : BaseController
    {
        private IUserRepository _userRepository;

        [ImportingConstructor]
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public JsonResult Index()
        {
            return success(string.Empty, new { entities = IndexUserViewModel.MapToViewModel(_userRepository.Index(u => u.Roles).ToList()) });
        }

        [HttpPut]
        public JsonResult Update(DetailUserViewModel userToUpdate)
        {
            if (ModelState.IsValid)
            {
                _userRepository.Update(userToUpdate.MapFromViewModel<ApplicationUser>());
                return success(MessageUtil.GenerateUpdateSuccessfull(App_GlobalResources.lang.user));
            }

            return unsuccess(ErrorUtil.GenerateModelStateError(ModelState));
        }

        [HttpDelete]
        public JsonResult Delete(string id)
        {
            if (ModelState.IsValid)
            {
                _userRepository.Delete(id);
                return success(App_GlobalResources.lang.userDeleted);
            }

            return unsuccess(ErrorUtil.GenerateModelStateError(ModelState));
        }

        [HttpGet]
        public JsonResult Read(string id)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = _userRepository.Read(id);

                if (user != null)
                {
                    return success(string.Empty, DetailUserViewModel.MapToViewModel(user));
                }
                else
                {
                    return unsuccess(string.Format(App_GlobalResources.lang.userCantRead, id));
                }
            }

            return unsuccess(ErrorUtil.GenerateModelStateError(ModelState));
        }

        [HttpPost]
        public async Task<JsonResult> Create(DetailUserViewModel newUser)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = Mapper.MapFromViewModel<ApplicationUser, DetailUserViewModel>(newUser);

                var result = await _userRepository.ValidateUser(user);

                if (result.Succeeded)
                {
                    user = _userRepository.Create(user);

                    return success(App_GlobalResources.lang.userCreated, null, new { id = user.Id });
                }

                return unsuccess(ErrorUtil.JoinErrors(result.Errors));
            }

            return unsuccess(ErrorUtil.GenerateModelStateError(ModelState));
        }

        [HttpPost]
        public JsonResult Invite(string emails)
        {
            if (!string.IsNullOrEmpty(emails))
            {
                List<string> emailCollection = emails.Split(';').ToList();

                foreach (string email in emailCollection)
                {
                    if (!_userRepository.EmailExit(email))
                    {
                        _userRepository.Create(new ApplicationUser() { Email = email, UserName = email, FirstName = email, LastName = email });
                    }
                }

                return success(App_GlobalResources.lang.inviteSuccess);
            }

            return unsuccess(App_GlobalResources.lang.inviteNoEmail);
        }
    }
}