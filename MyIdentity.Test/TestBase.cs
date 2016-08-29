using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyIdentity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIdentity.Test
{
    [TestClass]
    public abstract class TestBase
    {
        private static IUnityContainer _unityContainer = null;
        private static IServiceLocator _serviceLocator = null;
        private static User _user;
        private static Role _role;
        private static UserLogin _userLogin;

        private static IUnityContainer UnityContainer
        {
            get
            {
                return _unityContainer ?? (_unityContainer = UnityConfig.GetUnityContainer());
            }
        }

        private static IServiceLocator ServiceLocator
        {
            get
            {
                return _serviceLocator ?? (_serviceLocator = UnityContainer.Resolve<IServiceLocator>());
            }
        }
       
        protected TService GetService<TService>()
        {
            return ServiceLocator.Get<TService>();
        }

        public static User MyUser
        {
            get
            {
                return _user ?? (_user = new User
                {
                    UserID = Guid.NewGuid().ToString(),
                    UserName = "admin",
                    FirstName = "lester",
                    LastName = "echavez",
                    Email = "t@t.com",
                    PasswordHash = "lester!@#123"
                });
            }
        }

        public static Role MyRole
        {
            get
            {
                return _role ?? (_role = new Role { RoleID = Guid.NewGuid().ToString(), RoleName = "Admin" });
            }
        }
        public static UserLogin MyUserLogin
        {
            get
            {
                return _userLogin ?? (_userLogin = new UserLogin
                {
                    ProviderKey = Guid.NewGuid().ToString(),
                    LoginProvider = Guid.NewGuid().ToString(),
                    User = MyUser
                });
            }
        }

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
    }
}
