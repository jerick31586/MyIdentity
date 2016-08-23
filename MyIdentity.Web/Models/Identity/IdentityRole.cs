using System;
using Microsoft.AspNet.Identity;

namespace MyIdentity.Web.Models.Identity
{
    public class IdentityRole : IRole<string>
    {
        public IdentityRole()
        {
            Id = Guid.NewGuid().ToString();
        }
        public IdentityRole(string name)
            :this()
        {
            Name = name;
        }
        public string Id { get; set; }
        public string Name { get; set; }
    }
}