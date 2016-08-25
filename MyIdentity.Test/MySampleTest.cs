using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIdentity.Test.MyTest
{        
    public interface IBarService
    {
        string Bar();
    }

    public class BarService : IBarService
    {
        public string Bar()
        {
            return "Foo";
        }
    }

    public interface IServiceLocator
    {
        T Get<T>();
    }

    public interface IContainerRegistrationModule<T>
    {
        void Register(T container);
    }

    public abstract class DIServiceLocator<TContainer> : IServiceLocator
    {
        protected TContainer Container { get; private set; }

        protected DIServiceLocator(TContainer container)
        {
            Container = container;
        }
        public T Get<T>()
        {
            throw new NotImplementedException();
        }
    }

}
