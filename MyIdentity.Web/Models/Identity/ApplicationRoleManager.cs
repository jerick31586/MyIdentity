using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyIdentity.Web.Models.Identity
{    
    public class ApplicationRoleManager : RoleManager<IdentityRole, string>
    {

        public ApplicationRoleManager(IRoleStore<IdentityRole, string> store) : base(store)
        {
        }
    }
}