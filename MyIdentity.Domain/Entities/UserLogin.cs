using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIdentity.Domain.Entities
{
    public class UserLogin
    {
        #region Field
        private User _user;
        #endregion
        #region Scalar Properties                
        public virtual string LoginProvider { get; set; }
        public virtual string ProviderKey { get; set; }

        public virtual string UserID { get; set; }
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
