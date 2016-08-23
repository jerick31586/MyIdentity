using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIdentity.Domain.Entities
{
    public class User
    {
        #region Fields
        private ICollection<Role> _roles;
        private ICollection<UserClaim> _userClaims;
        private ICollection<UserLogin> _userLogins;
        #endregion

        #region Scalar Properties
        public string UserID { get; set; }
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
        #endregion

        #region Navigation Properties
        public virtual ICollection<Role> Roles
        {
            get { return _roles ?? (_roles = new List<Role>()); }
            set { _roles = value; }
        }
        public virtual ICollection<UserClaim> UserClaims
        {
            get { return _userClaims ?? (_userClaims = new List<UserClaim>()); }
            set { _userClaims = value; }
        }
        public virtual ICollection<UserLogin> UserLogins
        {
            get { return _userLogins ?? (_userLogins = new List<UserLogin>()); }
            set { _userLogins = value; }
        }
        #endregion
    }    
}
