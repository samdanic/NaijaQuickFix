using NaijaQuickFix.Models;
using NaijaQuickFix.Service;
using NaijaQuickFix.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NaijaQuickFix.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        //public ActionResult Index(RegisterViewModel rvm)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var accountService = new AccountService();
        //     Account adm =   accountService.Insert(rvm.ToModel(new Models.Account()));
        //        if (adm!= null)
        //        {
        //            return RedirectToAction("Index", "Home");
        //        }
        //        else{

        //            try
        //            {
        //                ViewBag.error = "Registration Error";
        //            }
        //            catch (Exception)
        //            {

        //                throw;
        //            }
        //        }
        //        //return Json(new { succcess = true, message = "Successfully Registered" });
                
        //    }
        //    return View(rvm);

        //}
    }
}