using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using UPC_Lunch.Models;

namespace UPC_Lunch.Helpers
{
    public static class NotificationHelper
    {
        public static int UserNotifications(IPrincipal User, bool isAuthenticated)
        {
            if (!User.IsInRole("Administrator"))
            {
                if (!User.IsInRole("Restaurant"))
                {
                    var db = new UPCLunchContext();

                    int nroNotificaciones = (from obj in db.Notifications
                                             where obj.Email == User.Identity.Name && obj.Seen == false
                                             select obj).Count();
                    db.Dispose();
                    return nroNotificaciones;
                }
            }
            return 0;
        }
    }
}