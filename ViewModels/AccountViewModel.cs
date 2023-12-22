using NaijaQuickFix.Models;
using NaijaQuickFix.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NaijaQuickFix.ViewModels
{
    public class AccountViewModel
    {

    }
    public class RegisterViewModel : IValidatableObject
    {
        [RegularExpression(@"^(?=.{3,100}$)([A-Za-z0-9][._()\[\]-]?)*$", ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "Invalid")]


        [StringLength(200)]
        [Display(Name = "First Name")]
        [Required(ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "Required")]
        public string FirstName { get; set; }

        [StringLength(200)]
        [Display(Name = "Last Name")]
        [Required(ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "Required")]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "Required")]
        public string Nin { get; set; }
        [StringLength(200)]
        [Display(Name = "Phone No.")]
        [Required(ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "Required")]
        public string PhoneNo { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "Required")]
        public string State { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "Required")]
        public string LGA { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "Required")]
        public string Gender { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "Required")]
        public string Password { get; set; }

        [Display(Name = "ConfirmPassword")]
        [Required(ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "Required")]
        [Compare("Password", ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "ConfirmPassword")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "Required")]
        [StringLength(100)]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "Required")]
       

        public DateTime? DOB { get; set; }
        //[Required(ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "Required")]
        //[StringLength(100)]
        //[Display(Name = "State")]
        //public string State { get; set; }
       
        [Display(Name = "Address")]

        public string Address { get; set; }



        public Account ToModel(Account acc)
        {
            acc.Phone = PhoneNo;
            acc.NIN = Nin;
            acc.Password = NaijaQuickFix.Helper.Helper.GetMD5(Password);
            acc.Email = Email;
            acc.FirstName = FirstName;
            acc.LastName = LastName;
            acc.Status = (int)NaijaQuickFix.Helper.Helper.Account_Status.Inactive;
            acc.LGA = LGA;
            acc.State = State;
            acc.AccountID = NaijaQuickFix.Helper.Helper.AdminId;
            acc.DOB = DOB;
            acc.GENDER = Gender;
            
            //acc.State = State;
            acc.Address = Address;
            acc.RoleId = (int)NaijaQuickFix.Helper.Helper.Account_Type_Enum.User;
            



            return acc;
        }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> res = new List<ValidationResult>();
            AccountService accountService = new AccountService();
            if (accountService.FindByEmail(Email) != null)
            {
                ValidationResult mss = new ValidationResult(@Resources.Messages.EmailExist);
                res.Add(mss);
            }
            if(Password!=ConfirmPassword)
            {
                ValidationResult mss = new ValidationResult("Retype the password does not match");
                res.Add(mss);
            }

            return res;
        }
    }
    public class LoginViewModel
    {
       
        [Required(ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "Required")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "Required")]
        public string Password { get; set; }
    }
    public class ChangeProfileViewModel
    {
        public Account Account { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "Required")]
        [StringLength(100)]
        [Display(Name = "Email")]
        public string Email { get; set; }

      
    
        [StringLength(200)]
        [Display(Name = "Phone")]
        [Required(ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "Required")]
        public string Phone { set; get; }

        [Required(ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "Required")]
        public string Password { get; set; }
        [Display(Name = "ConfirmPassword")]
        [Required(ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "Required")]
        [Compare("Password", ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = "ConfirmPassword")]
        public string ConfirmPassword { get; set; }
        public Account ToModel(Account acc)
        {
            acc.Id = Account.Id;
            acc.Email = Email;
            acc.Password = Helper.Helper.GetMD5(Password);
            acc.Phone = Phone;
            return acc;
        }
    }
}