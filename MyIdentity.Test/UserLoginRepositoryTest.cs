using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyIdentity.Data.EntityFramewok;
using MyIdentity.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIdentity.Test
{
    [TestClass]
    public class UserLoginRepositoryTest
    {
        private ApplicationDbContext _context;
        private IUserLoginRepository _repo;
        private List<string> _idList;
        [TestInitialize]
        public void Initialize()
        {
            TestHelper.UserLoginRepositoryInitialize(out _context, out _repo, out _idList);
        }

        [TestMethod]
        public void GetByProviderAndKey_WithUserProviderKeyAndLoginKeyInTheList_ShouldReturnUserLoginObject()
        {
            //Arrange
            string loginProviderInTheList = _idList[0],
                   providerKeyInTheList = _idList[1];

            //Act
            var result = _repo.GetByProviderAndKey(loginProviderInTheList, providerKeyInTheList);

            //Assert
            Assert.AreEqual(loginProviderInTheList, result.LoginProvider);
            Assert.AreEqual(providerKeyInTheList, result.ProviderKey);
        }
    }
}