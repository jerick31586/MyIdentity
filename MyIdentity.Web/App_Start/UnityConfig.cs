using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using MyIdentity.Domain;
using MyIdentity.Data.EntityFramewok;
using MyIdentity.Web.Models.Identity;
using Microsoft.AspNet.Identity;

namespace MyIdentity.Web
{
	public static class UnityConfig
	{
		public static void RegisterComponents()
		{
			var container = new UnityContainer();

			// register all your components with the container here
			// it is NOT necessary to register your controllers

			// e.g. container.RegisterType<ITestService, TestService>();

			container.RegisterType<ApplicationDbContext>(new InjectionConstructor("MyIdentityContext"));
			container.RegisterType<IUnitOfWork, UnitOfWork>();
			container.RegisterType<IUserStore<IdentityUser, string>, UserStore>(new TransientLifetimeManager());
			container.RegisterType<IRoleStore<IdentityRole, string>, RoleStore>(new TransientLifetimeManager());
			container.RegisterType<ApplicationUserManager>();
            container.RegisterType<ApplicationRoleManager>();

			DependencyResolver.SetResolver(new UnityDependencyResolver(container));
		}
	}
}