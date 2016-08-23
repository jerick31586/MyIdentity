using Effort;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyIdentity.Data.EntityFramewok;
using MyIdentity.Data.EntityFramewok.Repositories;
using MyIdentity.Domain.Entities;
using MyIdentity.Domain.Repositories;
using System;
using System.Collections.Generic;

namespace MyIdentity.Test
{    
    public static class TestHelper
    {    
        public static void AssertionThrow<TException>(Action blockToExecute) where TException : System.Exception
        {
            try
            {
                blockToExecute();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.GetType() == typeof(TException), "Expected exception of type " + typeof(TException) +
                    " but type of " + ex.GetType() + " was thrown instead.");
                return;
            }
            Assert.Fail("Expected exception of type " + typeof(TException) + " but no exception was thrown");
        }
        public static void RepositoryInitialize(out ApplicationDbContext context, out IRepository<User> repository, out List<string> idList)
        {
            var connection = DbConnectionFactory.CreateTransient();
            ApplicationDbContext _context = new ApplicationDbContext(connection);
            IRepository<User> _repository = new Repository<User>(_context);
            List<string> _idList = new List<string>();

            var user1 = new User
            {
                UserID = Guid.NewGuid().ToString(),
                UserName = "admin",
                FirstName = "lester",
                LastName = "echavez",
                Email = "t@t.com",
                PasswordHash = "lester!@#123"
            };
            _idList.Add(user1.UserID);

            _context.Users.Add(user1);
            var user2 = new User
            {
                UserID = Guid.NewGuid().ToString(),
                UserName = "admin1",
                FirstName = "lester1",
                LastName = "echavez1",
                Email = "t@t.com",
                PasswordHash = "lester!@#123"
            };
            _idList.Add(user2.UserID);
            _context.Users.Add(user2);
            _context.SaveChanges();

            context = _context;
            repository = _repository;
            idList = _idList;
            
        }
        public static void UserRepositoryInitialize(out ApplicationDbContext context, out IUserRepository repository, out List<string> idList)
        {
            var connection = DbConnectionFactory.CreateTransient();
            ApplicationDbContext _context = new ApplicationDbContext(connection);
            IUserRepository _repository = new UserRepository(_context);
            List<string> _idList = new List<string>();

            var user1 = new User
            {
                UserID = Guid.NewGuid().ToString(),
                UserName = "admin",
                FirstName = "lester",
                LastName = "echavez",
                Email = "t@t.com",
                PasswordHash = "lester!@#123"
            };
            _idList.Add(user1.UserID);

            _context.Users.Add(user1);
            var user2 = new User
            {
                UserID = Guid.NewGuid().ToString(),
                UserName = "admin1",
                FirstName = "lester1",
                LastName = "echavez1",
                Email = "c@c.com",
                PasswordHash = "lester!@#123"
            };
            _idList.Add(user2.UserID);
            _context.Users.Add(user2);
            _context.SaveChanges();

            context = _context;
            repository = _repository;
            idList = _idList;
        }
        public static void RoleRepositoryInitialize(out ApplicationDbContext context, out IRoleRepository repo)
        {
            var connection = DbConnectionFactory.CreateTransient();
            ApplicationDbContext _context = new ApplicationDbContext(connection);
            IRoleRepository _repo = new RoleRepository(_context);
            
            var role1 = new Role { RoleID = Guid.NewGuid().ToString(), RoleName = "Admin" };            
            var role2 = new Role { RoleID = Guid.NewGuid().ToString(), RoleName = "User" };

            _context.Roles.Add(role1);
            _context.Roles.Add(role2);
            _context.SaveChanges();

            context = _context;
            repo = _repo;
        }

        public static void UserLoginRepositoryInitialize(out ApplicationDbContext context, out IUserLoginRepository repo, out List<string> idLIst)
        {
            var connection = DbConnectionFactory.CreateTransient();
            ApplicationDbContext _context = new ApplicationDbContext(connection);
            IUserLoginRepository _repo = new UserLoginRepository(_context);
            List<string> _idList = new List<string>();
            var user1 = new User
            {
                UserID = Guid.NewGuid().ToString(),
                UserName = "admin",
                FirstName = "lester",
                LastName = "echavez",
                Email = "t@t.com",
                PasswordHash = "lester!@#123"
            };

            var userLogin1 = new UserLogin {
                ProviderKey = Guid.NewGuid().ToString(),
                LoginProvider = Guid.NewGuid().ToString(),
                User = user1
            };
            _idList.Add(userLogin1.LoginProvider);
            _idList.Add(userLogin1.ProviderKey);
            _context.UserLogins.Add(userLogin1);

            var user2 = new User
            {
                UserID = Guid.NewGuid().ToString(),
                UserName = "admin",
                FirstName = "lester",
                LastName = "echavez",
                Email = "t@t.com",
                PasswordHash = "lester!@#123"
            };
            var userLogin2 = new UserLogin
            {
                ProviderKey = Guid.NewGuid().ToString(),
                LoginProvider = Guid.NewGuid().ToString(),
                User = user2
            };
            _idList.Add(userLogin2.LoginProvider);
            _idList.Add(userLogin2.ProviderKey);

            _context.UserLogins.Add(userLogin2);
            _context.SaveChanges();

            context = _context;
            repo = _repo;
            idLIst = _idList;
        }
    }
}