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
    public class AuthActivity : ActionFilterAttribute, IExceptionFilter
    {        
        private ApplicationUserManager _userManager;        

        public AuthActivity()
        {
            _userManager = DependencyResolver.Current.GetService<ApplicationUserManager>();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var user = _userManager.FindByNameAsync(filterContext.HttpContext.User.Identity.Name);

            if (user.Result != null)
            {
                if (!_userManager.IsInRoleAsync(user.Result.Id, AccessLevel).Result)
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(
                            new { controller = "Error", action = "UnAuthorized" }    
                        ));
                }
            }
            
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            
        }
        public string AccessLevel { get; set; }

        public void OnException(ExceptionContext filterContext)
        {
            throw new NotImplementedException();
        }
    }
}