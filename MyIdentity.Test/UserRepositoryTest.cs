using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyIdentity.Domain.Repositories;
using MyIdentity.Data.EntityFramewok;
using System.Collections.Generic;

namespace MyIdentity.Test
{
    [TestClass]
    public class UserRepositoryTest
    {
        private ApplicationDbContext _context;
        private IUserRepository _repository;
        private List<string> _idList;

        [TestInitialize]
        public void Initialize()
        {
            TestHelper.UserRepositoryInitialize(out _context, out _repository, out _idList);
        }        
        
        [TestMethod]
        public void FindByUserName_WithUserNameInTheList_ShouldReturnUserObjectWithSameUserName()
        {
            //Arrange
            string expectedUserName = "admin";

            //Act
            var result = _repository.FindByUserName(expectedUserName);

            //Assert
            Assert.AreEqual(expectedUserName, result.UserName);
        }

        [TestMethod]
        public void FindByUserName_WithUserNotInTheList_ShouldReturnNullUserObject()
        {
            //Arrange
            string notInTheListUserName = "chloe";

            //Act
            var result = _repository.FindByUserName(notInTheListUserName);

            //Assert
            Assert.IsNull(result);
            
        }                
    }
}