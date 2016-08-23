using System;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using MyIdentity.Domain.Entities;
using MyIdentity.Domain.Repositories;
using System.Threading;

namespace MyIdentity.Data.EntityFramewok.Repositories
{
    public class UserLoginRepository : Repository<UserLogin>, IUserLoginRepository
    {
        public UserLoginRepository(ApplicationDbContext context) : base(context)
        {
        }

        public UserLogin GetByProviderAndKey(string loginProvider, string providerKey)
        {
            return Set.FirstOrDefault(x => x.LoginProvider == loginProvider && x.ProviderKey == providerKey);
        }

        public Task<UserLogin> GetByProviderAndKeyAsync(CancellationToken cancellationToken, string loginProvider, string providerKey)
        {
            return Set.FirstOrDefaultAsync(x => x.LoginProvider == loginProvider && x.ProviderKey == providerKey);
        }
    }
}
