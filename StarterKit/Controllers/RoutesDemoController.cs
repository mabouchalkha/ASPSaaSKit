using StarterKit.Repositories;
using System.Web.Mvc;

namespace StarterKit.Controllers
{
    [Authorize(Roles = "Admin, Owner, User")]
    public class RoutesDemoController : Controller
    {
        private ClientRepository _clientRepo = new ClientRepository();
        private UserRepository _userRepo = new UserRepository();

        public JsonResult One()
        {
            return Json(new { data = _userRepo.Read("160b84f4-4cfa-4bd5-a165-d4d0135fd45c") }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles="User, Owner")]
        public ActionResult Two()
        {
            return Json(new { data = _userRepo.Index() }, JsonRequestBehavior.AllowGet);
        }
    }
}