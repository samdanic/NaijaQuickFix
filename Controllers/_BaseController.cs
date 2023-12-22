using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NaijaQuickFix.Controllers
{
    public class _BaseController : Controller
    {
        // GET: _Base
        protected string UNKNOWN_ERROR_MSG = Resources.Messages.Anerror;
        // GET: Cart
        protected string GetErrorMessage()
        {
            var message = UNKNOWN_ERROR_MSG;
            var error = ModelState.Values.SelectMany(v => v.Errors).FirstOrDefault();
            if (error != null)
            {
                message = error.ErrorMessage;
            }

            return message;
        }
        protected ActionResult Error(string message = "")
        {
            if (string.IsNullOrEmpty(message))
                message = GetErrorMessage();

            return Json(new { success = false, content = message });
        }
        protected ActionResult ErrorAdmin(string message = "")
        {
            if (string.IsNullOrEmpty(message))
                message = GetErrorMessage();

            return Json(new { success = false, message = message });
        }
        protected ActionResult Success(string message = "", bool reload = false, string redirect = "")
        {
            return Json(new { success = true, message = message, reload = reload, redirect = redirect });
        }
    }
}
