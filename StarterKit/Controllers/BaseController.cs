using StarterKit.Utils;
using System.Web.Mvc;

namespace StarterKit.Controllers
{
    public abstract class BaseController : Controller
    {
        protected BaseController() { }

        protected JsonResult success(string info, object data = null, object meta = null, JsonRequestBehavior jsonRequestBehavior = JsonRequestBehavior.AllowGet)
        {
            return this.Json(new { success = true, type = "success", message = info, data = data, meta = meta }, jsonRequestBehavior);
        }

        protected JsonResult unsuccess(string info, object data = null, object meta = null, JsonRequestBehavior jsonRequestBehavior = JsonRequestBehavior.AllowGet, JsonStatus status = JsonStatus.s_500)
        {
            Response.StatusCode = (int)status;
            return this.Json(new { success = false, type = "error", message = info, data = data, meta = meta }, jsonRequestBehavior);
        }

        protected JsonResult info(string info, object data = null, object meta = null, JsonRequestBehavior jsonRequestBehavior = JsonRequestBehavior.AllowGet)
        {
            return this.Json(new { success = true, type = "info", message = info, data = data, meta = meta }, jsonRequestBehavior);
        }
    }
}
