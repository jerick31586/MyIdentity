using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIdentity.Domain.Entities
{
    public class UserClaim
    {
        #region Field
        private User _user;
        #endregion
        #region Scalar Properties                
        public virtual int ClaimID { get; set; }
        public virtual string UserID { get; set; }
        public virtual string ClaimType { get; set; }
        public virtual string ClaimValue { get; set; }
        #endregion
        #region Navigation Properties
        public virtual User User
        {
            get { return _user; }
            set { _user = value; }
        }
        #endregion
    }
}
