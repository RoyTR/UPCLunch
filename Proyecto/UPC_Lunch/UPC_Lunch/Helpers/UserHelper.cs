using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UPC_Lunch.Models;

namespace UPC_Lunch.Helpers
{
    public static class UserHelper
    {
        public static void RemoveUser(string UserMail)
        {
            using (var transaction = new ApplicationDbContext().Database.BeginTransaction())
            {
                ApplicationDbContext context = new ApplicationDbContext();
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                var user = userManager.FindByEmail(UserMail);
                var rolesForUser = userManager.GetRoles(user.Id);
                var logins = user.Logins;

                foreach (var login in logins.ToList())
                {
                    userManager.RemoveLogin(login.UserId, new UserLoginInfo(login.LoginProvider, login.ProviderKey));
                }
                if (rolesForUser.Count() > 0)
                {
                    foreach (var item in rolesForUser.ToList())
                    {
                        var result = userManager.RemoveFromRole(user.Id, item);
                    }
                }
                userManager.Delete(user);
                transaction.Commit();
            }
        }
    }
}