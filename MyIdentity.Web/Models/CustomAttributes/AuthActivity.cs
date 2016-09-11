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
    public interface IActionFilter<TAttribute> where TAttribute : Attribute
    {
        void OnActionExecuting(TAttribute attribute, ActionExecutingContext context);
    }


    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AuthActivityAttribute : Attribute
    {
        private ApplicationRoleManager _roleManager;

        public AuthActivityAttribute(ApplicationRoleManager roleManager)
        {
            _roleManager = roleManager;
        }
        
        public string AccessLevel { get; set; }
        public ApplicationRoleManager RoleManager
        {
            get { return _roleManager; }
            private set { _roleManager = value; }
        }
                        
    }
}