using MyIdentity.Domain.Entities;
using MyIdentity.Domain.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Data.Entity;

namespace MyIdentity.Data.EntityFramewok.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public User FindByEmail(string email)
        {
            return Set.FirstOrDefault(x => x.Email == email);
        }

        public Task<User> FindByEmailAsync(string email)
        {
            return Set.FirstOrDefaultAsync(x => x.Email == email);
        }

        public Task<User> FindByEmailAsync(CancellationToken cancellationToken, string email)
        {
            return Set.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
        }

        public User FindByUserName(string userName)
        {
            return Set.FirstOrDefault(x => x.UserName == userName);
        }

        public Task<User> FindByUserNameAsync(string userName)
        {
            return Set.FirstOrDefaultAsync(x => x.UserName == userName);
        }

        public Task<User> FindByUserNameAsync(CancellationToken cancellationToken, string userName)
        {
            return Set.FirstOrDefaultAsync(x => x.UserName == userName, cancellationToken);
        }
    }
}
