using NaijaQuickFix.Models;
using NaijaQuickFix.Security;
using NaijaQuickFix.Service;
using NaijaQuickFix.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using static NaijaQuickFix.Helper.Helper;

namespace NaijaQuickFix.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register(RegisterViewModel rvm)
        {
            if (ModelState.IsValid)
            {

                var accountService = new AccountService();
                string confirmationGuid = Guid.NewGuid().ToString();

                Account adm = accountService.Insert(rvm.ToModel(new Models.Account() {}));

                if (adm != null)
                {
                    string url = HttpContext.Request.Url.GetLeftPart(UriPartial.Authority) + "/Home/Verify?token=" + confirmationGuid;
                    string body = "hi" + " " + rvm.FirstName + ", " + Resources.Content.MailInfo + " <a href='" + url + "'>" + "Click Here" + "" + "Thank you.";
                    //NaijaQuickFix.Helper.Helper.SendMail(rvm.Email, Resources.Content.TitleMail + " " + Resources.Content.WebsiteName + "", body);

                    NaijaQuickFix.Helper.Helper.SendMail(rvm.Email, "Hey", body);


                    return Content("successfully registered, check your email for confirmation!.... Thank you.");
                 
                    //return RedirectToAction("Login", "Home");
                }
                else
                {

                    try
                    {
                        ViewBag.error = "Registration Error";
                    }
                    catch (Exception)
                    {

                        throw new InvalidOperationException();
                    }
                }
                return Json(new { succcess = true, message = "Successfully Registered" });

            }
            return View(rvm);


        }
        public ActionResult Login(LoginViewModel lvm)
        {
            if (ModelState.IsValid)
            {
                AccountService accountService = new AccountService();
                Account acc = accountService.Login(lvm.Email, NaijaQuickFix.Helper.Helper.GetMD5(lvm.Password));
                if (acc!= null)
                {
                    if (acc.Status ==(int)Account_Status.Inactive)
                    {
                        SimpleSessionPersister.SetAuthentication(acc);
                        return RedirectToAction("Index", "Home");
                    }
                    SimpleSessionPersister.SetAuthentication(acc);
                    return RedirectToAction("Index", "dashboard");
                }
                ViewBag.error = "Login Error";

            }
            return View(lvm);
        }
        public ActionResult Logout()
        {
            SimpleSessionPersister.SignOut();
            return RedirectToAction("Index", "Home");
        }
        #region
        [HttpPost]
        //public ActionResult Search(SearchViewModell search)
        //{
        //    var artisanService = new ArtisanService();
        //    var model = artisanService.SearchBothProfessionAndLocation(search.SearchProfession, search.SearchLocation);
        //    int c = model.Count();
        //    if (model == null)
        //    {
        //       var model1 = artisanService.SearchForProfession(search.SearchProfession);
        //        if (model1!=null)
        //        {
        //            //var content1 = RenderViewToString("../Home/SearchArtisanPartial", model1);
        //            //return Json(new { success = true, content = content1 });
        //            return View(model1);

        //        }
        //        else
        //        {
        //            return Content("No Result Found");
        //        }


        //    }
        //    //var content = RenderViewToString("../Home/SearchArtisanPartial", model);
        //    //return Json(new { success = true, content = content });
        //    return View(model);

        //}
        //string RenderViewToString(string viewName, object model)
        //{
        //    var context = this.ControllerContext;
        //    if (string.IsNullOrEmpty(viewName))
        //        viewName = context.RouteData.GetRequiredString("action");

        //    var viewData = new ViewDataDictionary(model);

        //    using (var sw = new StringWriter())
        //    {
        //        var viewResult = ViewEngines.Engines.FindPartialView(context, viewName);
        //        var viewContext = new ViewContext(context, viewResult.View, viewData, new TempDataDictionary(), sw);
        //        viewResult.View.Render(viewContext, sw);

        //        return sw.GetStringBuilder().ToString();
        //    }
        //}
        #endregion
        public ActionResult Search(string q, string m)
        {
            var artisanService = new ArtisanService();
            var model = artisanService.SearchBothProfessionAndLocation(q, m);
            int c = model.Count();
            if (model.Count() == 0)
            {
                var model1 = artisanService.SearchForProfession(q);
                if (model1 != null)
                {
                    var content1 = RenderViewToString("../Home/SearchArtisanPartial", model1);
                    return Json(new { success = true, content = content1 });


                }
                else
                {

                    return Json(new { success = false, content = "" });
                }


            }
            else
            {
                var content = RenderViewToString("../Home/SearchArtisanPartial", model);
                return Json(new { success = true, content = content });
            }

        }
        #region RenderViewToString
        string RenderViewToString(string viewName, object model)
        {
            var context = this.ControllerContext;
            if (string.IsNullOrEmpty(viewName))
                viewName = context.RouteData.GetRequiredString("action");

            var viewData = new ViewDataDictionary(model);

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(context, viewName);
                var viewContext = new ViewContext(context, viewResult.View, viewData, new TempDataDictionary(), sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
        #endregion

        //public ActionResult Verify(string token)
        //{
        //    var accountService = new AccountService();
        //    Account acc = accountService.FindByEmailToken(token);
        //    if (acc !=null)
        //    {
        //        accountService.Update(acc);
        //    }
        //    return RedirectToAction("Login", "Home");
        //    //return Content("Your account ready for use now. Thanks!");

        //}
    }
}