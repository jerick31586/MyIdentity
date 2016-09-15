using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIdentity.Domain.Entities
{
    public class Department
    {
        #region Fields
        private ICollection<User> _users;
        #endregion

        #region Scalar Properties
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        #endregion

        #region Navigation Properties
        public virtual ICollection<User> Users
        {
            get { return _users ?? ( _users = new List<User>()); }
            set { _users = value; }
        }
        #endregion

    }
}
