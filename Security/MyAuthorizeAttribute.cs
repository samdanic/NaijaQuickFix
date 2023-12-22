using NaijaQuickFix.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace NaijaQuickFix.Security
{
    public class MyAuthorizeAttribute: System.Web.Mvc.AuthorizeAttribute
    {
        public override void OnAuthorization(System.Web.Mvc.AuthorizationContext filterContext)
        {

            if (string.IsNullOrEmpty(SimpleSessionPersister.Account_Emails))
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Account", action = "Login" }));
            else
            {
                AccountService accountService = new AccountService();
                CustomPrincipal mp = new CustomPrincipal(accountService.FindByEmail(SimpleSessionPersister.Account_Emails));
                if (!mp.IsInRole(Roles))
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = "Index" }));
            }
        }
    }
}