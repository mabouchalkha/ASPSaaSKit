using StarterKit.Repositories;
using System.Web.Mvc;
using StarterKit.Mappers;
using System;
using StarterKit.ViewModels;
using StarterKit.DOM;

namespace StarterKit.Controllers
{
    public class UserController : BaseController
    {
        private UserRepository _userRepo = new UserRepository();

        [HttpGet]
        public JsonResult Index ()
        {
            return success("Users retrieved successfully", new { entities = _userRepo.Index().MapToIndexUserViewModels() });
        }

        [HttpPost]
        public JsonResult Update (IndexUserViewModel entity)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = _userRepo.Read(entity.Id);

                if (user != null)
                {
                    ApplicationUser updatedUser = entity.MapToApplicationUser(user);

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
                    return unsuccess(string.Format("Cannot read User with Id : {0}", entity.Id));
                }
            }

            return unsuccess("Something went wrong...");
        }
    }
}