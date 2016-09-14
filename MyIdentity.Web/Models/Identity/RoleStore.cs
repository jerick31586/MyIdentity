using Microsoft.AspNet.Identity;
using MyIdentity.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using MyIdentity.Domain.Entities;

namespace MyIdentity.Web.Models.Identity
{
    public class RoleStore : IRoleStore<IdentityRole, string>, IQueryableRoleStore<IdentityRole, string>        
    {
        private readonly IUnitOfWork _unitOfWork;

        

        public RoleStore(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Private Methods
        private Role getRole(IdentityRole role)
        {
            if (role == null)
                return null;

            var r = new Role();
            populateRole(r, role);

            return r;
        }        
        private void populateRole(Role role, IdentityRole identityRole)
        {
            role.RoleName = identityRole.Name;
        }

        private IdentityRole getIdentityRole(Role role)
        {
            if (role == null)
                return null;

            var identityRole = new IdentityRole();
            populateIdentityRole(identityRole, role);

            return identityRole;
        }
        private void populateIdentityRole(IdentityRole identityRole, Role role)
        {
            identityRole.Name = role.RoleName;
        }
        #endregion

        #region IRoleStore<IdentityRole, string> Members
        public Task CreateAsync(IdentityRole role)
        {
            if(role == null)
                throw new ArgumentNullException("role");

            var r = getRole(role);
            _unitOfWork.Roles.Add(r);
            return _unitOfWork.SaveChangesAsync();
        }
        public Task DeleteAsync(IdentityRole role)
        {
            if (role == null)
                throw new ArgumentNullException("role");

            var r = _unitOfWork.Roles.FindById(role.Id);
            if (r == null)
                throw new ArgumentException("IdentityRole does correspond to a role entity", "role");

            _unitOfWork.Roles.Remove(r);
            return _unitOfWork.SaveChangesAsync();
        }
        public void Dispose()
        {
            //Unity will handle for the disposal
        }
        public Task<IdentityRole> FindByIdAsync(string roleId)
        {
            var r = _unitOfWork.Roles.FindById(roleId);
            return Task.FromResult<IdentityRole>(getIdentityRole(r));
        }

        public Task<IdentityRole> FindByNameAsync(string roleName)
        {
            var r = _unitOfWork.Roles.FindByName(roleName);
            return Task.FromResult<IdentityRole>(getIdentityRole(r));
        }

        public Task UpdateAsync(IdentityRole role)
        {
            if (role == null)
                throw new ArgumentNullException("role");

            var r = _unitOfWork.Roles.FindById(role.Id);
            if (r == null)
                throw new ArgumentException("IdentityRole does not correspond to a role entity", "role");

            _unitOfWork.Roles.Update(r);
            return _unitOfWork.SaveChangesAsync();            
        }
        #endregion

        public IQueryable<IdentityRole> Roles
        {
            get
            {
                return _unitOfWork.Roles
                    .GetAll()
                    .Select(x => getIdentityRole(x))
                    .AsQueryable();
            }
        }
    }

}