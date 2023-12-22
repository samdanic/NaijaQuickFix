using NaijaQuickFix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace NaijaQuickFix.Security
{
    public class SimpleSessionPersister
    {
        public static bool IsAuthenticated
        {
            get
            {
                if (HttpContext.Current.Session != null && HttpContext.Current.Session["Account"] != null)
                {
                    return true;
                }
                return false;
            }
        }

        public static bool IsAdmin
        {
            get
            {
                return (IsAuthenticated && GetClaim(ClaimTypes.Role) == "Admin");
            }
        }

        public static bool IsUser
        {
            get
            {
                return (IsAuthenticated && GetClaim(ClaimTypes.Role) == "User");
            }
        }
           
        #region Profile Fields
        public static string ACCOUNT_STATUS = "NaijaQuickFix/ACCOUNT_STATUS";

        public static int Id
        {
            get { return GetClaimInt32(ClaimTypes.PrimarySid); }
        }

        public static string Account_Emails
        {
            get { return GetClaim(ClaimTypes.NameIdentifier); }
        }

        public static string Account_Phone
        {
            get { return GetClaim(ClaimTypes.HomePhone); }
        }

        public static string Account_Email
        {
            get { return GetClaim(ClaimTypes.Email); }
        }

        public static string Account_FirstName
        {
            get { return GetClaim(ClaimTypes.Name); }
        }

        public static string Role_Name
        {
            get { return GetClaim(ClaimTypes.Role); }
        }

        public static int Account_Status
        {
            get { return GetClaimInt32(ACCOUNT_STATUS); }
        }
        #endregion

        //Get Claims
        private static int GetClaimInt32(string claimType)
        {
            return int.Parse(GetClaim(claimType) ?? "0");
        }

        public static string GetClaim(string claimType)
        {
            if (HttpContext.Current.Session != null)
            {
                var acc = HttpContext.Current.Session["Account"] as List<Claim>;
                if (acc != null)
                {
                    var claim = acc.FirstOrDefault(c => c.Type == claimType);
                    if (claim != null)
                        return claim.Value;
                }
            }

            return null;
        }

        public static void SetAuthentication(Account acc)
        {
            if (acc.Id == 0)
                throw new ArgumentNullException("User Id");
            if (string.IsNullOrEmpty(acc.Email))
                throw new ArgumentNullException("User Email");
            if (string.IsNullOrEmpty(acc.FirstName))
                throw new ArgumentNullException("User FirstName");
            if (string.IsNullOrEmpty(acc.Phone))
                throw new ArgumentNullException("User PhoneNo"); 
            if (string.IsNullOrEmpty(acc.Role.Name))
                throw new ArgumentNullException("Role User");
            //if (acc.Account_Status == null)
            //    throw new ArgumentNullException("Trạng thái user");

            //create claim basic.
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.PrimarySid, acc.Id.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, acc.Email),
                    new Claim(ClaimTypes.Email, acc.Email),
                    new Claim(ClaimTypes.Name, acc.FirstName),
                        new Claim(ClaimTypes.HomePhone, acc.Phone),
                    new Claim(ClaimTypes.Role, acc.Role.Name),
                    new Claim(ACCOUNT_STATUS, acc.Status.ToString())
                };

            HttpContext.Current.Session["Account"] = claims;
        }

        public static void SignOut()
        {
            HttpContext.Current.Session["Account"] = null;
        }
    }
}