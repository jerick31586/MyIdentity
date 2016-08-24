using MyIdentity.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyIdentity.Test
{
    [TestClass]
    public class UnitOfWorkTest
    {
        [TestInitialize]
        public void Initialize()
        {
            TestHelper.RegisterComponents();
        }
          
        //public UnitOfWorkTest(IUnitOfWork unitOfWork)
        //{

        //    _unitOfWork = unitOfWork;
        //}


    }
}
