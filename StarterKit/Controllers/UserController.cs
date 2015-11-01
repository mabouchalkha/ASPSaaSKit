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
            return success(string.Empty, new { entities = _userRepository.Index(u => u.Roles, u=> u.Tenant).ToList().MapToIndexUserViewModels() });
        }

        [HttpPut]
        public JsonResult Update(DetailUserViewModel userToUpdate)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = _userRepository.Read(userToUpdate.Id);

                if (user != null)
                {
                    ApplicationUser updatedUser = userToUpdate.MapToApplicationUser(user);
                    updatedUser = _userRepository.Update(updatedUser);

                    return success("User successfully updated");
                }
                else
                {
                    return unsuccess(string.Format("Cannot find user with Id : {0}", userToUpdate.Id));
                }
            }

            return unsuccess(ErrorUtil.GenerateModelStateError(ModelState));
        }

        [HttpDelete]
        public JsonResult Delete (string id)
        {
            if (ModelState.IsValid)
            {
                var currentUser = UserHelper.GetCurrentUser();

                if (currentUser.Id != id)
                {
                    _userRepository.Delete(id);

                    return success("User successfully deleted");
                }
                else
                {
                    return unsuccess("You cannot delete your own user");
                }
            }

            return unsuccess(ErrorUtil.GenerateModelStateError(ModelState));
        }

        [HttpGet]
        public JsonResult Read (string id)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = _userRepository.Read(id);

                if (user != null)
                {
                    return success(string.Empty, user.MapToDetailUserViewModel());
                }
                else
                {
                    return unsuccess(string.Format("Cannot retrieve user with id {0}", id));
                }
            }

            return unsuccess(ErrorUtil.GenerateModelStateError(ModelState));
        }

        [HttpPost]
        public async Task<JsonResult> Create (DetailUserViewModel newUser)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = newUser.MapToApplicationUser();

                var result = await _userRepository.ValidateUser(user);

                if (result.Succeeded)
                {
                    user = _userRepository.Create(user);

                    return success("User created successfully", null, new { id = user.Id });
                }

                return unsuccess(ErrorUtil.JoinErrors(result.Errors));
            }

            return unsuccess(ErrorUtil.GenerateModelStateError(ModelState));
        }

        [HttpPost]
        public JsonResult Invite (string emails)
        {
            if (!string.IsNullOrEmpty(emails))
            {
                List<string> emailCollection = emails.Split(';').ToList();

                foreach (string email in emailCollection)
                {
                    if (!_userRepository.EmailExit(email))
                    {
                        _userRepository.Create(new ApplicationUser() { Email = email, UserName = email });
                    }
                }

                return success("Users has been invited. They will receive an email to confirm their account");
            }

            return unsuccess("You need to put some emails adresses");
        }
    }
}