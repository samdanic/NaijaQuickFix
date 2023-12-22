
using NaijaQuickFix.Models;
using NaijaQuickFix.Security;
using NaijaQuickFix.Service;
using NaijaQuickFix.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static NaijaQuickFix.Helper.Helper;

namespace NaijaQuickFix.Controllers
{
    public class DashboardController : _BaseController
    {
        //public Object artisanService;
        public DashboardController()
        {
            //this.artisanService = new ArtisanService();
        }
        public const int PageSize = 6;
        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult Profile()
        //{
        //    var model = new ChangeProfileViewModel();
        //    AccountService accountService = new AccountService();
        //    Account acc = accountService.SelectByID(SimpleSessionPersister.Id);
        //    if (acc != null)
        //    {
        //        model.Email = acc.Email;
        //        model.Password = acc.Password;
        //        model.ConfirmPassword = acc.Password;
        //        model.Phone = acc.Phone;
        //    }
        //    else
        //    {
        //        return RedirectToAction("Login", "Account");
        //    }
        //    return View(model);
        //}

       
        public new ActionResult Profile(ChangeProfileViewModel model)   
        {
            if (ModelState.IsValid)
            {
                AccountService accountService = new AccountService();
                Account acc = accountService.SelectByID(SimpleSessionPersister.Id);
                if (acc != null)
                {
                    acc = model.ToModel(acc);
                    accountService.Update(acc);
                    ViewBag.success = "changed";
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            return View(model);

        }

        public ActionResult New()
        {
            return View();
        }

        public ActionResult Addartisans(RegisterArtisanViewModel registerArtisan)
        {
            if (!ModelState.IsValid)
            {

                //AssociationService associationService = new AssociationService();
                //var assoList = associationService.SelectAll();
                //GenderService genderService = new GenderService();
                //var genderList = genderService.SelectAll();
                //ProfessionService professionService = new ProfessionService();
                //var professionList = professionService.SelectAll();
                var viewM = new RegisterArtisanViewModel
                {

                    Associations = new AssociationService().SelectAll(),
                    Gender = new GenderService().SelectAll(),
                    Professions = new ProfessionService().SelectAll()
                    
                };

                return View(viewM);

            }
            else
            {
                ArtisanService artSer = new ArtisanService();
                if (artSer.FindByNIN(registerArtisan.Nin) != null)
                {
                    ViewBag.error = "The Nin has already been used, Kindly you yours";
                }
                var artisann = registerArtisan.ToModel(new Models.Artisan());
                ArtisanService artisanService2 = new ArtisanService();
                var art = artisanService2.Insert(artisann);
                if (art != null)
                {
                    return RedirectToAction("allartisans", "Dashboard");

                }
                else
                {
                    ViewBag.error = "ewooo";
                }
            }
            if (registerArtisan.Artisan.Id == 0)
            {
                try
                {
                    ArtisanService artisanService1 = new ArtisanService();
                    artisanService1.Insert(registerArtisan.ToModel(new Models.Artisan()));
                    if (artisanService1 != null)
                    {
                        return RedirectToAction("allartisans", "Dashboard");
                    }
                    else
                    {
                        ViewBag.error = "your code ooo!!!!";
                    }
                }
                catch (SqlException e)
                {

                    Console.WriteLine(e);
                }

            }


            return View(registerArtisan);


        }


        public ActionResult Allartisans(int page = 1, EntityManagementFilterEnum currentFilter = EntityManagementFilterEnum.Public)
        {

            var artisanService = new ArtisanService();
            var model = artisanService.SelectPaging(null, 0, 0);

            switch (currentFilter)
            {
                case EntityManagementFilterEnum.All:
                    {
                        model = artisanService.SelectPaging(null, page, PageSize);
                        break;
                    }
                case EntityManagementFilterEnum.Public:
                    {
                        model = artisanService.SelectPaging(p => p.Status == (int)EntityManagementFilterEnum.Public, page, PageSize);
                        break;
                    }
                case EntityManagementFilterEnum.UnPublic:
                    {
                        model = artisanService.SelectPaging(p => p.Status == (int)EntityManagementFilterEnum.UnPublic, page, PageSize);
                        break;
                    }
            }
            ViewBag.CurrentFilter = currentFilter;

            return View(model);
        }
        public ActionResult users(int page = 1)
        {
            var account1 = new AccountService();
            var model1 = account1.SelectPaging(null, page, PageSize);
            return View(model1);
        }
        public ActionResult Complaints()
        {
            return View();
        }
     
        //public ActionResult AddArtisanImageToDb(int Id = 0)
        //{
        //    var artisanService = new ArtisanService();
        //    Artisan artisan = artisanService.SelectByID(Id);
        //    if (artisan == null)
        //    {
        //        artisan = new Artisan();
        //    }
        //    return View(new ImageAndArtisanViewModel() { Artisan = artisan });
        //}
      
     
        
        //public ActionResult AddArtisanImageToDb(ImageAndArtisanViewModel ImgArtViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var artisanService = new ArtisanService();
        //        var artisan = new Artisan();
        //        if (ImgArtViewModel.Artisan.Id != 0)
        //        {
        //            artisan = artisanService.SelectByID(ImgArtViewModel.Artisan.Id);
        //        }
        //        artisan = ImgArtViewModel.ToModel(artisan);
        //        if (ImgArtViewModel.Artisan.Id == 0)
        //        {
        //            artisanService.Insert(artisan);
        //        }
        //        else
        //        {
        //            artisanService.Update(artisan);
        //        }
        //        return Success("successfully added");
        //    }
        //    return ErrorAdmin();
        //}
     

        public ActionResult ArtisanDetails(int Id)
        {
            var artisanService = new ArtisanService();
            Artisan artisan = artisanService.SelectByID(Id);
            if (artisan == null)
            {
                artisan = new Artisan();
            }
            return View(new ImageAndArtisanViewModel() { Artisan = artisan });
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult GetAA(ImageAndArtisanViewModel ImgArtViewModel)
        {
            if (ModelState.IsValid)
            {
                var artisanService = new ArtisanService();
                var artisan = new Artisan();
                if (ImgArtViewModel.Artisan.Id != 0)
                {
                    artisan = artisanService.SelectByID(ImgArtViewModel.Artisan.Id);
                }
                artisan = ImgArtViewModel.ToModel(artisan);
                if (ImgArtViewModel.Artisan.Id == 0)
                {
                    artisanService.Insert(artisan);
                }
                else
                {
                    artisanService.Update(artisan);
                }
                return Success("successfully added");
            }
            return ErrorAdmin();
        }

        public ActionResult UnPublicArtisan(int id)
        {
            var service = new ArtisanService();
            var item = service.SelectByID(id);
            if (item != null)
            {
                item.Status = (int)Entity_Status.UnPublic;
                service.Update(item);
                return Json(new { success = true });
            }
            return Json(new { success = false, message = "Does Not Exist" });
        }

        public ActionResult PublicArtisan(int id)
        {
            var service = new ArtisanService();
            var item = service.SelectByID(id);
            if (item != null)
            {
                item.Status = (int)Entity_Status.Public;
                service.Update(item);
                return Json(new { success = true });
            }
            return Json(new { success = false, message = "Does Not Exist" });


        }


    }
}