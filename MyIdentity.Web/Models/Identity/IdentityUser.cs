using System;
using Microsoft.AspNet.Identity;

namespace MyIdentity.Web.Models.Identity
{
    public class IdentityUser : IUser<string>
    {
        public IdentityUser()
        {
            Id = Guid.NewGuid().ToString();
        }
        public IdentityUser(string userName)
            : this()
        {
            UserName = userName;
        }
        public string Id { get; set; }
        public string UserName { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
                
        public DateTime? DateOfBirth { get; set; }
        public virtual string PasswordHash { get; set; }
        public virtual string SecurityStamp { get; set; }
        public virtual bool EmailConfirmed { get; set; }
    }
}