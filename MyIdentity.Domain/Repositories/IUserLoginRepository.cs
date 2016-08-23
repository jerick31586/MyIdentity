using MyIdentity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyIdentity.Domain.Repositories
{
    public interface IUserLoginRepository : IRepository<UserLogin>
    {
        UserLogin GetByProviderAndKey(string loginProvider, string providerKey);
        Task<UserLogin> GetByProviderAndKeyAsync(CancellationToken cancellationToken, string loginProvider, string providerKey);
    }
}
