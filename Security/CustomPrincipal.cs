using NaijaQuickFix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.ApplicationServices;

namespace NaijaQuickFix.Security
{
    public class CustomPrincipal
    {
        public IIdentity Identity
        {
            get;
            set;
        }

        private Account Account;

        public CustomPrincipal(Account Account)
        {
            this.Account = Account;
            this.Identity = new GenericIdentity(Account.Email);
        }

        public bool IsInRole(string role)
        {
            RoleService roleService = new RoleService();
            var roles = role.Split(new char[] { ',' });
            return roles.Any(r => Account.Role.Name == r);
        }
    }
}