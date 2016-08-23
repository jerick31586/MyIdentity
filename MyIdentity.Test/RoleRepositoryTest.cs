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
    public class RoleRepositoryTest
    {
        private ApplicationDbContext _context;
        private IRoleRepository _repository;

        [TestInitialize]
        public void Initialize()
        {
            TestHelper.RoleRepositoryInitialize(out _context,out _repository);
        }

        [TestMethod]
        public void FindByName_WithRoleNameInTheList_ShouldReturnRoleObjectWithSameRoleName()
        {
            //Arrange
            var roleNameInTheList = "Admin";

            //Act
            var result = _repository.FindByName(roleNameInTheList);

            //Assert
            Assert.AreEqual(roleNameInTheList, result.RoleName);
        }

        [TestMethod]
        public void FindByName_WithRoleNameNotInTheList_ShouldReturnNulRoleObject()
        {
            //Arrange
            var roleNameInTheList = "Super Admin";

            //Act
            var result = _repository.FindByName(roleNameInTheList);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        //[ExpectedException(typeof(NullReferenceException))]
        public void FindByName_WithParameterLess_ShouldReturnNullReferenceException()
        {
            //Arrange
            Role role = null;

            //Act
            //var result = _repository.FindByName(role.RoleName);

            //Assert
            TestHelper.AssertionThrow<NullReferenceException>(() => _repository.FindByName(role.RoleName));
            
        }
    }
}