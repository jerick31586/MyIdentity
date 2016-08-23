using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace MyIdentity.Web.Controllers.CustomController
{
    //Custom ClaimsPrincipal
    public class AppUser : ClaimsPrincipal
    {        

        public AppUser(ClaimsPrincipal principal)
            : base(principal)
        {
        }
        public string UserName
        {
            get { return FindFirst("username").Value; }
        }
        public string FullName
        {
            get { return FindFirst("firstname").Value + " " + FindFirst("lastname").Value; }            
        }        
    }

    //Custom Controller
    public class AppController : Controller
    {
        public AppUser CurrentUser
        {
            get { return new AppUser(User as ClaimsPrincipal); }
        }
    }

    //Customer View Page
    public abstract class AppViewPage<TModel> : WebViewPage<TModel>
    {
        public AppUser CurrentUser
        {
            get { return new AppUser(User as ClaimsPrincipal); }
        }
    }

    public abstract class AppViewPage : AppViewPage<dynamic>
    {
    }
}