using MyIdentity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyIdentity.Domain.Repositories
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        Department FindByName(string departmentName);
        Task<Department> FindByNameAsync(string departmentName);
        Task<Department> FindByNameAsync(CancellationToken token, string departmentName);
    }
}
