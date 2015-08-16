using StarterKit.Architecture.Bases;
using StarterKit.DOM;
using StarterKit.Helpers;
using StarterKit.Mappers;
using StarterKit.Repositories;
using StarterKit.Utils;
using StarterKit.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace StarterKit.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        private UserRepository _userRepo;

        public UserController(UserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpGet]
        public JsonResult Index()
        {
            return success(string.Empty, new { entities = _userRepo.Index().MapToIndexUserViewModels() });
        }

        [HttpPut]
        public JsonResult Update(DetailUserViewModel userToUpdate)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = _userRepo.Read(userToUpdate.Id);

                if (user != null)
                {
                    ApplicationUser updatedUser = userToUpdate.MapToApplicationUser(user);

                    if (_userRepo.HasPendingChange(updatedUser))
                    {
                        bool isUpdated = _userRepo.Update(updatedUser);

                        if (isUpdated)
                        {
                            return success("User successfully updated");
                        }
                        else
                        {
                            return unsuccess("Cannot save this user. Please refresh and try again");
                        }
                    }
                    else
                    {
                        return info("User has no pending change");
                    }
                }
                else
                {
                    return unsuccess(string.Format("Cannot find user with Id : {0}", userToUpdate.Id));
                }
            }

            return unsuccess(ErrorUtil.DefaultError);
        }

        [HttpDelete]
        public JsonResult Delete (string id)
        {
            if (ModelState.IsValid)
            {
                var currentUser = UserHelper.GetCurrentUser();

                if (currentUser.Id != id)
                {
                    bool isDeleted = _userRepo.Delete(id);

                    if (isDeleted)
                    {
                        return success("User successfully deleted");
                    }
                    else
                    {
                        return unsuccess("User delete unsuccessfully. Please try again");
                    }
                }
                else
                {
                    return unsuccess("You cannot delete your own user");
                }
            }

            return unsuccess(ErrorUtil.DefaultError);
        }

        [HttpGet]
        public JsonResult Read (string id)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = _userRepo.Read(id);

                if (user != null)
                {
                    return success(string.Empty, user.MapToDetailUserViewModel());
                }
                else
                {
                    return unsuccess(string.Format("Cannot retrieve user with id {0}", id));
                }
            }

            return unsuccess(ErrorUtil.DefaultError);
        }

        [HttpPost]
        public async Task<JsonResult> Create (DetailUserViewModel newUser)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = newUser.MapToApplicationUser();

                var result = await _userRepo.ValidateUser(user);

                if (result.Succeeded)
                {
                    bool hasCreated = _userRepo.Create(user);

                    if (hasCreated)
                    {
                        return success("User created successfully", null, new { id = user.Id });
                    }
                    else
                    {
                        return unsuccess("User creation failed");
                    }
                }

                return unsuccess(string.Join("<br />", result.Errors));
            }

            return unsuccess(ErrorUtil.DefaultError);
        }

        [HttpPost]
        public JsonResult Invite (string emails)
        {
            if (!string.IsNullOrEmpty(emails))
            {
                List<string> emailCollection = emails.Split(';').ToList();

                foreach (string email in emailCollection)
                {
                    if (!_userRepo.EmailExit(email))
                    {
                        _userRepo.Create(new ApplicationUser() { Email = email, UserName = email });
                    }
                }

                return success("Users has been invited. They will receive an email to confirm their account");
            }

            return unsuccess("You need to put some emails adresses");
        }
    }
}