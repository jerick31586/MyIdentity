using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyIdentity.Data.EntityFramewok;
using MyIdentity.Domain.Entities;
using MyIdentity.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIdentity.Test
{
    [TestClass]
    public class RepositoryTest
    {
        private ApplicationDbContext _context;
        private IRepository<User> _repository;
        private List<string> _idList;


        [TestInitialize]
        public void Initialize()
        {
            TestHelper.RepositoryInitialize(out _context, out _repository, out _idList);
        }

        [TestMethod]
        public void GetAll_WithParameterLess_ShouldReturnUserList()
        {
            //Arrange            
            int expected = 2;

            //Act
            var result = _repository.GetAll();

            //Assert                       
            Assert.AreEqual(result.Count, expected);
        }
        [TestMethod]
        public void FindById_WithUserIdInTheList_ShouldReturnUserID()
        {
            //Arrange
            string expectedUserID = _idList[0];

            //Act
            var result = _repository.FindById(expectedUserID);

            //Assert
            Assert.AreEqual(expectedUserID, result.UserID);
        }
        [TestMethod]
        public void FindById_WithUserIDNotInTheList_ShouldReturnNull()
        {
            //Arrange
            var notInTheLIstUserID = "1";

            //Act
            var result = _repository.FindById(notInTheLIstUserID);

            //Assert
            Assert.IsNull(result);
        }
    }
}
