using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using MyIdentity.Domain;
using MyIdentity.Domain.Entities;

namespace MyIdentity.Web.Models.Identity
{
    public class UserStore : IUserStore<IdentityUser,string>, IUserClaimStore<IdentityUser, string>, IUserRoleStore<IdentityUser, string>,
        IUserLoginStore<IdentityUser, string>, IUserPasswordStore<IdentityUser, string>, IUserSecurityStampStore<IdentityUser, string>,
        IUserEmailStore<IdentityUser, string>, IQueryableUserStore<IdentityUser, string>, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;

        public IQueryable<IdentityUser> Users
        {
            get
            {
                return _unitOfWork.Users
                    .GetAll()
                    .Select(x => getIdentityUser(x))
                    .AsQueryable();
            }
        }

        public UserStore(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Private Methods
        private User getUser(IdentityUser identityUser)
        {
            if (identityUser == null)
                return null;

            var u = new User();
            populateUser(u, identityUser);

            return u;
        }

        private void populateUser(User user, IdentityUser identityUser)
        {
            user.UserID = identityUser.Id;
            user.UserName = identityUser.UserName;
            user.FirstName = identityUser.FirstName;
            user.LastName = identityUser.LastName;
            user.PhoneNumber = identityUser.PhoneNumber;
            user.PasswordHash = identityUser.PasswordHash;
            user.SecurityStamp = identityUser.SecurityStamp;
            user.Email = identityUser.Email;
            user.Address = identityUser.Address;
            user.DateOfBirth = identityUser.DateOfBirth;
        }

        private IdentityUser getIdentityUser(User user)
        {
            if (user == null)
                return null;

            var identityUser = new IdentityUser();
            populateIdentityUser(identityUser, user);

            return identityUser;
        }

        private void populateIdentityUser(IdentityUser identityUser, User user)
        {
            identityUser.Id = user.UserID;
            identityUser.UserName = user.UserName;
            identityUser.FirstName = user.FirstName;
            identityUser.LastName = user.LastName;
            identityUser.PhoneNumber = user.PhoneNumber;
            identityUser.PasswordHash = user.PasswordHash;
            identityUser.SecurityStamp = user.SecurityStamp;
            identityUser.Email = user.Email;
            identityUser.Address = user.Address;
            identityUser.DateOfBirth = user.DateOfBirth;
        }
        #endregion              

        #region IUserStore
        public Task CreateAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            var u = getUser(user);
            _unitOfWork.Users.Add(u);
            return _unitOfWork.SaveChangesAsync();
        }

        public Task DeleteAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var u = _unitOfWork.Users.FindById(user.Id);

            if (u == null)
                throw new ArgumentException("IdentityUser does not correspond to a user Identity");

            _unitOfWork.Users.Remove(u);
            return _unitOfWork.SaveChangesAsync();
        }

        public void Dispose()
        {
            //will be handled by Unity
        }

        public Task<IdentityUser> FindByIdAsync(string userId)
        {
            var user = _unitOfWork.Users.FindById(userId);
            return Task.FromResult(getIdentityUser(user));
        }

        public Task<IdentityUser> FindByNameAsync(string userName)
        {
            var user = _unitOfWork.Users.FindByUserName(userName);
            return Task.FromResult(getIdentityUser(user));
        }

        public Task UpdateAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            
            var u = _unitOfWork.Users.FindById(user.Id);

            if (u == null)
                throw new ArgumentException("Argument does not correspond to a User Identity", "user");

            populateUser(u, user); // I forgot this

            _unitOfWork.Users.Update(u);
            return _unitOfWork.SaveChangesAsync();
        }
        #endregion

        #region IUserClaimStore
        public Task<IList<Claim>> GetClaimsAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var u = _unitOfWork.Users.FindById(user.Id);
            if (u == null)
                throw new ArgumentException("Identity does not correspond to a user entity", "user");

            var c = u.UserClaims.Select(x => new Claim(x.ClaimType, x.ClaimValue)).ToList();
            return Task.FromResult<IList<Claim>>(c);
        }

        public Task AddClaimAsync(IdentityUser user, Claim claim)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (claim == null)
                throw new ArgumentNullException("claim");

            var u = _unitOfWork.Users.FindById(user.Id);
            if (u == null)
                throw new ArgumentException("IdentityUser does not correspond to a user entity", "user");
            var c = new UserClaim
            {
                ClaimType = claim.Type,
                ClaimValue = claim.Value,
                User = u
            };
            u.UserClaims.Add(c);
            _unitOfWork.Users.Update(u);
            return _unitOfWork.SaveChangesAsync();
        }

        public Task RemoveClaimAsync(IdentityUser user, Claim claim)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (claim == null)
                throw new ArgumentNullException("claim");

            var u = _unitOfWork.Users.FindById(user.Id);
            if (u == null)
                throw new ArgumentException("IdentityUser does not correspond to a user entity", "user");

            var c = u.UserClaims.FirstOrDefault(x => x.ClaimType == claim.Type && x.ClaimValue == claim.Value);
            u.UserClaims.Remove(c);

            _unitOfWork.Users.Update(u);
            return _unitOfWork.SaveChangesAsync();
        }
        #endregion

        #region IUserLoginStore
        public Task AddLoginAsync(IdentityUser user, UserLoginInfo login)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (login == null)
                throw new ArgumentNullException("login");
            var u = _unitOfWork.Users.FindById(user.Id);
            if (u == null)
                throw new ArgumentException("IdentityUser does not correspond to a User entity", "user");

            var loginProvider = new UserLogin
            {
                LoginProvider = login.LoginProvider,
                ProviderKey = login.ProviderKey,
                User = u
            };
            u.UserLogins.Add(loginProvider);
            _unitOfWork.Users.Update(u);
            return _unitOfWork.SaveChangesAsync();

        }
        public Task RemoveLoginAsync(IdentityUser user, UserLoginInfo login)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (login == null)
                throw new ArgumentNullException("login");

            var u = _unitOfWork.Users.FindById(user.Id);
            if (u == null)
                throw new ArgumentException("IdentityUser does not correspond to a User entity", "user");

            var loginProvider = u.UserLogins.FirstOrDefault(x => x.LoginProvider == login.LoginProvider && x.ProviderKey == login.ProviderKey);
            u.UserLogins.Remove(loginProvider);

            _unitOfWork.Users.Update(u);
            return _unitOfWork.SaveChangesAsync();
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var u = _unitOfWork.Users.FindById(user.Id);
            if (u == null)
                throw new ArgumentException("IdentityUser does not correspond to a User entity", "user");

            var loginProviders = u.UserLogins.Select(x => new UserLoginInfo(x.LoginProvider, x.ProviderKey)).ToList();
            return Task.FromResult<IList<UserLoginInfo>>(loginProviders);
        }

        public Task<IdentityUser> FindAsync(UserLoginInfo login)
        {
            if (login == null)
                throw new ArgumentNullException("login");

            var identityUser = default(IdentityUser);

            var loginProvider = _unitOfWork.UserLogins.GetByProviderAndKey(login.LoginProvider, login.ProviderKey);
            if (loginProvider != null)
                identityUser = getIdentityUser(loginProvider.User);

            return Task.FromResult<IdentityUser>(identityUser);
        }
        #endregion

        #region IUserRoleStore
        public Task AddToRoleAsync(IdentityUser user, string roleName)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (string.IsNullOrEmpty(roleName))
                throw new ArgumentException("Argument cannot be null or empty", "roleName");

            var u = _unitOfWork.Users.FindById(user.Id);
            if (u == null)
                throw new ArgumentException("IdentityUser does correspond to a User entity", "user");

            var r = _unitOfWork.Roles.FindByName(roleName);
            if (r == null)
                throw new ArgumentException("roleName does correspond to a role entity", "roleName");

            u.Roles.Add(r);

            _unitOfWork.Users.Update(u);
            return _unitOfWork.SaveChangesAsync();

        }

        public Task RemoveFromRoleAsync(IdentityUser user, string roleName)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (string.IsNullOrEmpty(roleName))
                throw new ArgumentException("Argument cannot be null or empty", "roleName");

            var u = _unitOfWork.Users.FindById(user.Id);
            if (u == null)
                throw new ArgumentException("IdentityUser does correspond to a User entity", "user");

            var r = _unitOfWork.Roles.FindByName(roleName);

            if (r == null)
                throw new ArgumentException("roleName does correspond to a role entity", "roleName");

            u.Roles.Remove(r);

            _unitOfWork.Users.Update(u);
            return _unitOfWork.SaveChangesAsync();
        }

        public Task<IList<string>> GetRolesAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var u = _unitOfWork.Users.FindById(user.Id);
            if (u == null)
                throw new ArgumentException("IdentityUser does not correspond to a user entity", "user");

            var roles = u.Roles.Select(x => x.RoleName).ToList();
            return Task.FromResult<IList<string>>(roles);
        }

        public Task<bool> IsInRoleAsync(IdentityUser user, string roleName)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (string.IsNullOrEmpty(roleName))
                throw new ArgumentException("Argument cannot be null or empty", "roleName");

            var u = _unitOfWork.Users.FindById(user.Id);
            if (u == null)
                throw new ArgumentException("IdentityUser does correspond to a User entity", "user");

            return Task.FromResult<bool>(u.Roles.Any(x => x.RoleName == roleName));
        }
        #endregion

        #region IUserPasswordStore
        public Task SetPasswordHashAsync(IdentityUser user, string passwordHash)
        {
            
            user.PasswordHash = passwordHash;            
            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            return Task.FromResult<string>(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            return Task.FromResult<bool>(!string.IsNullOrWhiteSpace(user.PasswordHash));
        }
        #endregion

        #region IUserSecurityStampStore
        public Task SetSecurityStampAsync(IdentityUser user, string stamp)
        {
            user.SecurityStamp = stamp;
            return Task.FromResult(0);
        }

        public Task<string> GetSecurityStampAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            return Task.FromResult(user.SecurityStamp);
        }
        #endregion

        #region IUserEmailStore<IdentityUser, string> Members
        public Task SetEmailAsync(IdentityUser user, string email)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            user.Email = email;
            return Task.FromResult(0);            
        }

        public Task<string> GetEmailAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(IdentityUser user, bool confirmed)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            user.EmailConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public Task<IdentityUser> FindByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException("email");

            var u = _unitOfWork.Users.FindByEmail(email);
            return Task.FromResult<IdentityUser>(getIdentityUser(u));
        }
        #endregion
    }
}