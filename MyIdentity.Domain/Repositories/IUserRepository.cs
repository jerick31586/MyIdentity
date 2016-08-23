using MyIdentity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyIdentity.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User FindByUserName(string userName);
        Task<User> FindByUserNameAsync(string userName);
        Task<User> FindByUserNameAsync(CancellationToken cancellationToken, string userName);
        User FindByEmail(string email);
        Task<User> FindByEmailAsync(string email);
        Task<User> FindByEmailAsync(CancellationToken cancellationToken, string email);
    }
}
