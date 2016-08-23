using MyIdentity.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyIdentity.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        #region Properties
        IUserRepository Users { get; }
        IRoleRepository Roles { get; }
        IUserLoginRepository UserLogins { get; }
        #endregion

        #region Methods
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        #endregion

    }
}
