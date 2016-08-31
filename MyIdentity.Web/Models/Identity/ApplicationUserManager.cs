using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using MyIdentity.Data.EntityFramewok;
using MyIdentity.Web.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyIdentity.Web.Models.Identity
{
    public class ApplicationUserManager : UserManager<IdentityUser, string>
    {
        public ApplicationUserManager(IUserStore<IdentityUser, string> store) 
            : base(store)
        {
            var dataProtectionProvider = Startup.DataProtectionProvider;

            UserValidator = new UserValidator<IdentityUser>(this)
            {
                AllowOnlyAlphanumericUserNames = true,
                RequireUniqueEmail = true
            };

            PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6
            };
            
            ClaimsIdentityFactory = new AppClaimsIdentityFactory();
            
            DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);

            if (dataProtectionProvider != null)
            {
                IDataProtector dataProtector = dataProtectionProvider.Create("ASP.NET Identity");
                UserTokenProvider = new DataProtectorTokenProvider<IdentityUser>(dataProtector);
            }
        }        
    }


    //Custom validation like Age 
    public class AppUserValidator : UserValidator<IdentityUser>
    {
        public AppUserValidator(UserManager<IdentityUser, string> manager) : base(manager)
        {
        }
        public override Task<IdentityResult> ValidateAsync(IdentityUser item)
        {
            var result = base.ValidateAsync(item);

            //var errors = 
            return result;
            
        }
    }

    public class AppPasswordValidator : PasswordValidator
    {

    }
    public class AppClaimsIdentityFactory : ClaimsIdentityFactory<IdentityUser>
    {
        public AppClaimsIdentityFactory()
        {

            //UserIdClaimType = "subject"; // If you're gonna change this you have to add AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier in Global Asax;
            UserNameClaimType = "username";
            RoleClaimType = "role";
        }
        public async override Task<ClaimsIdentity> CreateAsync(UserManager<IdentityUser, string> manager, IdentityUser user, string authenticationType)
        {
            var id = await base.CreateAsync(manager, user, authenticationType);            
            id.AddClaim(new Claim("firstname", user.FirstName));
            id.AddClaim(new Claim("lastname", user.LastName));
            return id;
        }
    }
}