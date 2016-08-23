using Effort;
using MyIdentity.Data.EntityFramewok;
using MyIdentity.Data.EntityFramewok.Repositories;
using MyIdentity.Domain.Entities;
using MyIdentity.Domain.Repositories;
using System;
using System.Collections.Generic;

namespace MyIdentity.Test
{    
    public class TestHelper
    {        
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
    }
}