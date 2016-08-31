using MyIdentity.Data.EntityFramewok;
using MyIdentity.Domain;
using MyIdentity.Web.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyIdentity.Web.Models.CustomAttributes
{    
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AuthActivityAttribute : Attribute
    {
        private string _activity;
        private readonly ApplicationUserManager _mgr;        
        public AuthActivityAttribute(string activity, ApplicationUserManager manager)
        {
            _activity = activity;
            _mgr = manager;
        }

        public string Activity {
            get { return _activity; }
        }        
        public static void AuthenticateUser(string activity)
        {
            
        }        
    }
}