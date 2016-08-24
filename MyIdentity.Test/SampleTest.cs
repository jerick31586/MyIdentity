using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIdentity.Test
{
    public interface IFooService
    {
        string Bar();
    }

    public class FooService : IFooService
    {
        public string Bar()
        {
            return "Bar";
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

    public abstract class DependencyInjectionServiceLocator<TContainer> : IServiceLocator
    {
        //DI conatiner
        protected TContainer Container { get; private set; }

        public DependencyInjectionServiceLocator(TContainer container)
        {
            Container = container;
        }
        public virtual T Get<T>()
        {
            return Get<T>(Container);
        }
        protected abstract T Get<T>(TContainer container);
    }

    public class CustomUnityServiceLocator : DependencyInjectionServiceLocator<IUnityContainer>
    {
        public CustomUnityServiceLocator(IUnityContainer container) : base(container)
        {
        }

        protected override T Get<T>(IUnityContainer container)
        {
            return this.Container.Resolve<T>();
        }
    }

    public class UnityRegistrationModule : IContainerRegistrationModule<IUnityContainer>
    {
        public void Register(IUnityContainer container)
        {
            //Register Service locator
            container.RegisterType<IServiceLocator, CustomUnityServiceLocator>();
            //Register services
            container.RegisterType<IFooService, FooService>();
        }
        
    }
    
    public class UnityConfig
    {
        private static readonly Lazy<IUnityContainer> Container = new Lazy<IUnityContainer>(()=> 
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        public static IUnityContainer GetUnityContainer()
        {
            return Container.Value;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            var registrationModuleAssemblyName =
                System.Configuration.ConfigurationManager.AppSettings["UnityRegistrationModule"];

            var type = Type.GetType(registrationModuleAssemblyName);
            var module = (IContainerRegistrationModule<IUnityContainer>)Activator.CreateInstance(type);

            module.Register(container);
        }
    }
}
