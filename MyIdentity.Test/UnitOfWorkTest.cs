using MyIdentity.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyIdentity.Domain.Entities;
using System;
using System.Collections.Generic;

namespace MyIdentity.Test
{
    [TestClass]
    public class UnitOfWorkTest : TestBase
    {
        private IUnitOfWork _unitOfWork;
        private List<string> _idList;

        [TestInitialize]
        public void Initialize()
        {
            _unitOfWork = GetService<IUnitOfWork>();
            _idList = new List<string>();
        }

        [TestMethod] 
        public void Add_WithUserObject_ShouldReturnOneWhenSaved()
        {
            //Arrange
            var expectedValue = 1;

            //Act
            _unitOfWork.Users.Add(MyUser);
            var result = _unitOfWork.SaveChanges();

            //Assert
            Assert.AreEqual(expectedValue, result);
        }

        [TestMethod]
        public void FindByUserName_WithUserNameInTheList_ShouldReturnUserObjectWithSameUserName()
        {
            //Arrange
            string expectedUserName = "admin";

            //Act
            _unitOfWork.Users.Add(MyUser);
            _unitOfWork.SaveChanges();
            var result = _unitOfWork.Users.FindByUserName(expectedUserName);

            //Assert
            Assert.AreEqual(expectedUserName, result.UserName);
        }

        [TestMethod]
        public void FindByUserName_WithUserNotInTheList_ShouldReturnNullUserObject()
        {
            //Arrange
            string notInTheListUserName = "chloe";

            //Act
            var result = _unitOfWork.Users.FindByUserName(notInTheListUserName);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void FindByEmail_WithUserInTheList_ShouldReturnUserObjectWithSameEmail()
        {
            //Arrange
            string emailInTheList = "t@t.com";

            //Act
            _unitOfWork.Users.Add(MyUser);
            _unitOfWork.SaveChanges();
            var result = _unitOfWork.Users.FindByEmail(emailInTheList);

            //Assert
            Assert.AreEqual(emailInTheList, result.Email);
        }

        [TestMethod]
        public void FindByEmail_WithUserNotInTheList_ShouldReturnNullUserObject()
        {
            //Arrange
            string emailNotInTheList = "test@test.com";

            //Act
            var result = _unitOfWork.Users.FindByEmail(emailNotInTheList);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetAll_WithParameterLess_ShouldReturnUserList()
        {
            //Arrange            
            int expected = 1;

            //Act
            var result = _unitOfWork.Users.GetAll();

            //Assert                       
            Assert.AreEqual(result.Count, expected);
        }
        [TestMethod]
        public void FindById_WithUserIdInTheList_ShouldReturnUserID()
        {
            //Arrange
            string expectedUserID = MyUser.UserID;

            //Act
            var result = _unitOfWork.Users.FindById(expectedUserID);

            //Assert
            Assert.AreEqual(expectedUserID, result.UserID);
        }
        [TestMethod]
        public void FindById_WithUserIDNotInTheList_ShouldReturnNull()
        {
            //Arrange
            var notInTheLIstUserID = "1";

            //Act
            var result = _unitOfWork.Users.FindById(notInTheLIstUserID);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Add_WithRoleObject_ShouldReturnOneWhenSaved()
        {
            //Arrange
            var expectedValue = 1;

            //Act
            _unitOfWork.Roles.Add(MyRole);
            var result = _unitOfWork.SaveChanges();

            //Assert
            Assert.AreEqual(expectedValue, result);
        }
        [TestMethod]
        public void FindByName_WithRoleNameInTheList_ShouldReturnRoleObjectWithSameRoleName()
        {
            //Arrange
            var roleNameInTheList = "Admin";

            //Act
            var result = _unitOfWork.Roles.FindByName(roleNameInTheList);

            //Assert
            Assert.AreEqual(roleNameInTheList, result.RoleName);
        }

        [TestMethod]
        public void FindByName_WithRoleNameNotInTheList_ShouldReturnNulRoleObject()
        {
            //Arrange
            var roleNameInTheList = "Super Admin";

            //Act
            var result = _unitOfWork.Roles.FindByName(roleNameInTheList);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void FindByName_WithParameterLess_ShouldReturnNullReferenceException()
        {
            //Arrange
            Role role = null;

            //Act
            var result = _unitOfWork.Roles.FindByName(role.RoleName);

            //Assert
            AssertionThrow<NullReferenceException>(() => _unitOfWork.Roles.FindByName(role.RoleName));
        }

        //}
        [TestMethod]
        public void Add_WithUserLoginObject_ShouldReturnOneWhenSaved()
        {
            //Arrange
            var expectedValue = 1;

            //Act
            _unitOfWork.Users.Add(MyUser);            
            var user = _unitOfWork.Users.FindById(MyUser.UserID);
            user.UserLogins.Add(MyUserLogin);                        
            var result = _unitOfWork.SaveChanges();

            //Assert
            Assert.AreEqual(expectedValue, result);
        }
        [TestMethod]
        public void GetByProviderAndKey_WithUserProviderKeyAndLoginKeyInTheList_ShouldReturnUserLoginObject()
        {
            //Arrange
            string loginProviderInTheList = MyUserLogin.LoginProvider,
                   providerKeyInTheList = MyUserLogin.ProviderKey;

            //Act    
            _unitOfWork.Users.Add(MyUser);                                                
            var user = _unitOfWork.Users.FindById(MyUser.UserID);
            user.UserLogins.Add(MyUserLogin);
            _unitOfWork.SaveChanges();
            var result = _unitOfWork.UserLogins.GetByProviderAndKey(loginProviderInTheList, providerKeyInTheList);
            
            //Assert
            Assert.AreEqual(loginProviderInTheList, result.LoginProvider);
            Assert.AreEqual(providerKeyInTheList, result.ProviderKey);
        }
    }
}
