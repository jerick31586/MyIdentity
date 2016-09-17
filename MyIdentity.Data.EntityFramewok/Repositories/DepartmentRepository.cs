using MyIdentity.Domain.Entities;
using MyIdentity.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Data.Entity;

namespace MyIdentity.Data.EntityFramewok.Repositories
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {        
        public DepartmentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Department FindByName(string departmentName)
        {
            return Set.FirstOrDefault(x=>x.DepartmentName == departmentName);
        }

        public Task<Department> FindByNameAsync(string departmentName)
        {
            return Set.FirstOrDefaultAsync(x=>x.DepartmentName == departmentName);
        }

        public Task<Department> FindByNameAsync(CancellationToken token, string departmentName)
        {
            return Set.FirstOrDefaultAsync(x => x.DepartmentName == departmentName, token);
        }
    }
}
