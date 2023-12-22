using NaijaQuickFix.Models;
using NaijaQuickFix.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NaijaQuickFix.ViewModels
{
    public class ArtisanViewModel
    {
 
    }
    public class SearchViewModell
    {
        public string SearchProfession { get; set; }
        public string SearchLocation { get; set; }

    }
    public class RegisterArtisanViewModel : IValidatableObject
    {
        // 
        public Artisan Artisan { get; set; }
        public HttpPostedFileBase uploadFile { get; set; }
        public IEnumerable<Association> Associations { get; set; }
        public IEnumerable<Profession> Professions { get; set; }
        public IEnumerable<Gender> Gender { get; set; }

        //[RegularExpression(@"^(?=.{3,100}$)([A-Za-z0-9][._()\[\]-]?)*$", ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "Invalid")]


        [StringLength(200)]
        [Display(Name = "First Name")]
        [Required(ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "Required")]
        public string FullName { get; set; }


        [StringLength(200)]
        [Display(Name = "Company Name")]
        [Required(ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "Required")]
        public string CompanyName { get; set; }

        [StringLength(200)]
        [Display(Name = "Office Address")]
        [Required(ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "Required")]
        public string OfficeAddress { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "Required")]
        public string Nin { get; set; }
        [StringLength(200)]
        [Display(Name = "Phone No.")]
        [Required(ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "Required")]
        public string PhoneNo { get; set; }

        //[Required(ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "Required")]
        //public string State { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "Required")]
        public string LGA { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "Required")]
        public string State { get; set; }


        [Required(ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "Birthday Date")]
        public DateTime? BirthDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "Next of Kin")]
        public string NextOfKin { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "House Address")]
        public string HouseAddress { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "Job Description")]
        public string JobDescription { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "Required")]
        [StringLength(100)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        

        public Artisan ToModel(Artisan art)
        {

            //art.ArtisanProfilePhotoUrl = Helper.Helper.SaveAs("~/Images", uploadFile);
            art.Status = Helper.Helper.ArtisanStatus;
            art.Phone = PhoneNo;
            art.NIN = Nin;
            art.Email = Email;
            art.FullName = FullName;
            art.LGA = LGA;
            art.State = State;
            art.HouseAddress = HouseAddress;
            art.OfficeAddress = OfficeAddress;
            art.NextOfKinPhoneNo = NextOfKin;
            art.JobDescription = JobDescription;
            art.CompanyName = CompanyName;
            art.BirthDate = BirthDate;
            art.DateRegistered = DateTime.Now;
            art.BirthDate = BirthDate;
            art.AssociationId = Artisan.AssociationId;
            art.GenderId = Artisan.GenderId;
            art.ProfessionId = Artisan.ProfessionId;
            return art;
        }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> res = new List<ValidationResult>();
            ArtisanService artisanService = new ArtisanService();
            if (artisanService.FindByNIN(Nin) != null)
            {
                ValidationResult mss = new ValidationResult("NIN already exist, use yours please");
                
                res.Add(mss);
            }



            return res;
        }
    }
}