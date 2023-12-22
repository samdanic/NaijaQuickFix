using NaijaQuickFix.Models;
using NaijaQuickFix.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaijaQuickFix.Service
{
    public class AccountService: GenericRepository<Account>
    {
        public Account FindByEmail(string email)
        {
            return table.Where(p => p.Email == email).FirstOrDefault();
        }
        public Account Login(string email, string password)
        {
            return table.Where(l => l.Email == email && l.Password == password).FirstOrDefault();
        }
        //public Account FindByEmailToken(string token)
        //{
        //    return table.Where(p => p.EmailToken == token).FirstOrDefault();
        //}
    }
}