using Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using MyIdentity.Data.EntityFramewok;
using MyIdentity.Web.Models.Identity;
using Microsoft.Owin.Security.DataProtection;

[assembly:OwinStartup(typeof(MyIdentity.Web.Startup))]
namespace MyIdentity.Web
{
    public class Startup
    {
        internal static IDataProtectionProvider DataProtectionProvider { get; private set; }
        public void Configuration(IAppBuilder app)
        {

            DataProtectionProvider = app.GetDataProtectionProvider();

            app.UseCookieAuthentication(new CookieAuthenticationOptions {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Account/Login")                
            });
        }
    }
}