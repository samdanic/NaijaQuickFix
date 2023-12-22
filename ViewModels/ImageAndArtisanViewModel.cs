using NaijaQuickFix.Models;
using NaijaQuickFix.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NaijaQuickFix.ViewModels
{
    public class ImageAndArtisanViewModel
    {

        public Artisan Artisan { get; set; }
        public Artisan ToModel(Artisan art)
        {
            art.Id = Artisan.Id;
            art.FullName = Artisan.FullName;
            art.JobDescription = Artisan.JobDescription;
            art.Phone = Artisan.Phone;
            ProcessImageUpdate(Artisan.files, art, Artisan.pastfiles);
            return art;
        }
        private bool ProcessImageUpdate(
    IEnumerable<HttpPostedFileBase> files, Artisan artisan,
    List<int> pastfiles)
        {
            var imageArtisanService = new ImageService();
            try
            {
                if (Artisan.Id != 0)
                {
                    if (pastfiles == null)
                    {
                        pastfiles = new List<int>();
                    }
                    foreach (var ip in Artisan.Images.Select(i => i.Id))
                    {
                        if (!pastfiles.Contains(ip))
                        {
                            imageArtisanService.Delete(ip);
                        }
                    }

                }
                if (files != null)
                {
                    foreach (var item in files)
                    {
                        if (item != null)
                        {
                            var productPhoto = new Image();
                            productPhoto.Status = (int)Helper.Helper.Entity_Status.Public;
                            productPhoto.Url = NaijaQuickFix.Helper.Helper.SaveAs("~/Images/", item);
                            artisan.Images.Add(productPhoto);
                        }
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> res = new List<ValidationResult>();
            if (Artisan.pastfiles == null && Artisan.files == null)
            {
                ValidationResult mss = new ValidationResult("image required");
                res.Add(mss);
            }

            return res;
        }
    }
}
