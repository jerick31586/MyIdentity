using System;
using MyIdentity.Data.EntityFramewok.Configuration;
using MyIdentity.Domain.Entities;
using System.Data.Common;
using System.Data.Entity;

namespace MyIdentity.Data.EntityFramewok
{    
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("name=MyIdentityContext")
        {
        }

        public ApplicationDbContext(string nameOrConnectionString)
            :base(nameOrConnectionString)
        {            
        }

        public ApplicationDbContext(DbConnection connection)
            :base(connection, true)
        {
            Configuration.LazyLoadingEnabled = false;
        }
        public IDbSet<User> Users { get; set; }
        public IDbSet<Role> Roles { get; set; }
        public IDbSet<UserLogin> UserLogins { get; set; }       
        public IDbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new UserClaimConfiguration());
            modelBuilder.Configurations.Add(new UserLoginConfiguration());
            modelBuilder.Configurations.Add(new DepartmentConfiguration());
            base.OnModelCreating(modelBuilder);
        }
        public void Seed(ApplicationDbContext context)
        {
            context.Users.Add(new User
            {
                UserID = Guid.NewGuid().ToString(),
                UserName = "admin",
                FirstName = "lester",
                LastName = "echavez",
                Email = "t@t.com",
                PasswordHash = "lester!@#123"
            });
            context.Users.Add(new User
            {
                UserID = Guid.NewGuid().ToString(),
                UserName = "admin1",
                FirstName = "lester1",
                LastName = "echavez1",
                Email = "t@t.com",
                PasswordHash = "lester!@#123"
            });
            context.SaveChanges();
        }       
    }

    public class AlwaysCreateInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            context.Seed(context);
            base.Seed(context);
        }
    }
}