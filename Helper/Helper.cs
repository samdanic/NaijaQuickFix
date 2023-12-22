using NaijaQuickFix.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;

namespace NaijaQuickFix.Helper
{
   
    public class Helper
    {

        public enum EntityManagementFilterEnum
        {
            Public = 1,
            UnPublic = 0,
            All = 2
        }
        public enum Account_Status
        {
            Active = 1,
            Inactive = 0
        }
        public enum Account_Type_Enum
        {
            Admin = 1,
            User = 2
        }
        public enum Gender_Type
        {
            Male = 1,
            Female = 2
        }

        public enum Entity_Status
        {
            Public = 1,
            UnPublic = 0,
        }
        public static string GetFilter(EntityManagementFilterEnum filter)
        {
            switch (filter)
            {
                case EntityManagementFilterEnum.All:
                    {
                        return "All";
                    }
                case EntityManagementFilterEnum.Public:
                    {
                        return "Public";
                    }
                case EntityManagementFilterEnum.UnPublic:
                    {
                        return "UnPublic";
                    }
            }
            return "Public";

        }

        public static readonly int  ACCOUNT_Status_Users = 0;
        public static readonly int ACCOUNT_Status_Admin = 1;
        public static readonly int ArtisanStatus = 1;

        public static int GetAllUsersNumber()
        {
            AccountService account = new AccountService();
            return account.SelectAll().Where(a => a.Status == ACCOUNT_Status_Users).Count();
        }
        public static int GetAllArtisansByNumber()
        {
            var artisanByNumber = new ArtisanService();
            return artisanByNumber.SelectAll().Count();
        }
        public static int GetAllRegistered()
        {
           return GetAllUsersNumber() + GetAllArtisansByNumber();
        }

        public static int GetArtisanRegisteredToday()
        {
            var artisanByNumber = new ArtisanService();
             //new DateTime(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month,1);
            return artisanByNumber.SelectAll().Where(a => a.DateRegistered.Date == DateTime.Today.Date).Count();
        }

        public static int GetArtisanRegisteredLastMonth()
        {
            var artisanByNumber = new ArtisanService();
            var n = DateTime.Now.AddMonths(-2);
            n = new DateTime(n.Year, n.Month, 1);
            return artisanByNumber.SelectAll().Where(a => new DateTime(a.DateRegistered.AddMonths(-1).Year, a.DateRegistered.AddMonths(-1).Month, 1) == n.Date).Count();
        }
        public static readonly string AdminId = Guid.NewGuid().ToString();

        #region Encrypt MD5
        public static string GetMD5(string text)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            byte[] result = md5.Hash;
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                str.Append(result[i].ToString("x2"));
            }
            return str.ToString();
        }
        #endregion

        #region File Upload
        public static string SaveAs(string image_path, HttpPostedFileBase uploadFile)
        {
            var id = Guid.NewGuid().ToString();

            var StorageRoot = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(image_path));

            var ext = Path.GetExtension(uploadFile.FileName);
            var fullPath = StorageRoot + id + "" + ext;
            var fullName = id + "" + ext;

            uploadFile.SaveAs(fullPath);

            return fullName;
        }

        //public static object GetArtisanProfilePhoto(int id, string photo)
        //{
        //    ArtisanService artisanService  = new ArtisanService();
        //    return artisanService.FindBothIdAndPhoto(id, photo); ;
        //}
        #endregion

        public async static Task<bool> SendMail(string ToEmail, string Subject, string Body)
        {
            
             SettingService settingService = new SettingService();

            string myname = "www.aswol.org";

            string email = settingService.SelectByID("YourEmail").Value;
            string password = settingService.SelectByID("PasswordYourEmail").Value;
            string server = settingService.SelectByID("Server").Value;
            int port = Convert.ToInt32(settingService.SelectByID("Port").Value);
            NetworkCredential loginInfo = new NetworkCredential(email, password);
            MailMessage msg = new MailMessage();
            SmtpClient smtpClient = new SmtpClient(server, port);
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = loginInfo;
            try
            {
                //WebMail.SmtpServer = settingService.SelectByID("Server").Value;
                //WebMail.SmtpPort = Convert.ToInt32(settingService.SelectByID("Port").Value);
                //WebMail.UserName = settingService.SelectByID("YourEmail").Value;
                //WebMail.Password = settingService.SelectByID("PasswordYourEmail").Value;
                //WebMail.From = settingService.SelectByID("YourEmail").Value;

                //WebMail.Send(to: ToEmail, subject: Subject, body: Body);
                 msg.From = new MailAddress(myname,email);
                msg.To.Add(new MailAddress(ToEmail));
                msg.Bcc.Add(new MailAddress(email));
                msg.Subject = Subject;
                //msg.Sender = new MailAddress(email); 

                msg.Body = Body;
                msg.IsBodyHtml = true;
                smtpClient.Send(msg);
                return true;
            }
            catch (Exception)
            {
                return false;


            }
        }
    }
}
