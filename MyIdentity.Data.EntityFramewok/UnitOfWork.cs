using MyIdentity.Domain;
using System.Threading.Tasks;
using MyIdentity.Domain.Repositories;
using System.Threading;
using MyIdentity.Data.EntityFramewok.Repositories;

namespace MyIdentity.Data.EntityFramewok
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Fields
        private readonly ApplicationDbContext _context;
        private IUserRepository _userRepository;
        private IRoleRepository _roleRepository;
        private IUserLoginRepository _userLoginRepository;
        #endregion        
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        public IUserLoginRepository UserLogins
        {
            get
            {
                return _userLoginRepository ?? (_userLoginRepository = new UserLoginRepository(_context));
            }
        }

        public IRoleRepository Roles
        {
            get
            {
                return _roleRepository ?? (_roleRepository = new RoleRepository(_context));
            }
        }

        public IUserRepository Users
        {
            get
            {
                return _userRepository ?? (_userRepository = new UserRepository(_context));
            }
        }

        public void Dispose()
        {
            _userRepository = null;
            _roleRepository = null;
            _userLoginRepository = null;
            _context.Dispose();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
