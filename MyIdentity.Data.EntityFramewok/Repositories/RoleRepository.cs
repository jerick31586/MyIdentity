using MyIdentity.Domain.Entities;
using MyIdentity.Domain.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Data.Entity;

namespace MyIdentity.Data.EntityFramewok.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Role FindByName(string roleName)
        {
            return Set.FirstOrDefault(x => x.RoleName == roleName);
        }

        public Task<Role> FindByNameAsync(string roleName)
        {
            return Set.FirstOrDefaultAsync(x => x.RoleName == roleName);
        }

        public Task<Role> FindByNameAsync(CancellationToken cancellationToken, string roleName)
        {
            return Set.FirstOrDefaultAsync(x => x.RoleName == roleName, cancellationToken);
        }
    }
}
