using StarterKit.Utils;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Routing;
using System.Text;
using System;

namespace StarterKit.Architecture.Bases
{
    public abstract class BaseController : Controller
    {
        protected BaseController() { }

        protected JsonResult success(string info, object data = null, object meta = null, JsonRequestBehavior jsonRequestBehavior = JsonRequestBehavior.AllowGet)
        {
            return this.CustomJson(new { success = true, type = "success", message = info, data = data, meta = meta }, jsonRequestBehavior);
        }

        protected JsonResult unsuccess(string info, object data = null, object meta = null, JsonRequestBehavior jsonRequestBehavior = JsonRequestBehavior.AllowGet, JsonStatus status = JsonStatus.s_500)
        {
            Response.StatusCode = (int)status;
            return this.CustomJson(new { success = false, type = "error", message = info, data = data, meta = meta }, jsonRequestBehavior);
        }

        protected JsonResult info(string info, object data = null, object meta = null, JsonRequestBehavior jsonRequestBehavior = JsonRequestBehavior.AllowGet)
        {
            return this.CustomJson(new { success = true, type = "info", message = info, data = data, meta = meta }, jsonRequestBehavior);
        }

        protected CustomJsonResult CustomJson(object data, JsonRequestBehavior behavior)
        {
            return new CustomJsonResult
            {
                Data = data,
                ContentType = "application/json",
                JsonRequestBehavior = behavior,
            };
        }

        [Obsolete("Please use CustomJson instead")]
        protected CustomJsonResult Json(object data, JsonRequestBehavior behavior)
        {
            throw new InvalidOperationException("This function is obselete");
        }
    }
}
