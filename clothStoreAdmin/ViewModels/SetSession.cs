using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace clothStoreAdmin.ViewModels
{
    public class SetSession
    {
        public static void SetUserSession(string userId, string designation, string fullname)
        {
            HttpContext.Current.Session["userId"] = userId;
            HttpContext.Current.Session["designation"] = designation;
            HttpContext.Current.Session["fullname"] = fullname;
        }
    }
    public class UserSession
    {
        public string userId { get { return HttpContext.Current.Session["userId"].ToString(); } set { userId = HttpContext.Current.Session["userId"].ToString(); } }
        public string designation { get { return HttpContext.Current.Session["designation"].ToString(); } set { designation = HttpContext.Current.Session["designation"].ToString(); } }
        public string fullName { get { return HttpContext.Current.Session["fullname"].ToString(); } set { designation = HttpContext.Current.Session["fullname"].ToString(); } }
    }
}