using System.Web.Mvc;
using CompositionRoot;
using Microsoft.AspNet.Identity;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;
using WebSite.Managers;
using WebSite.Models;

namespace WebSite
{
    public static class Bootstrapper
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();
 
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
 
            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<IUserStore<IdentityUser>, UserStore>();
            container.RegisterType<IUserManager<IdentityUser>, ApplicationUserManager>();

            IocBuilder.BuildContainer(container);

            return container;
        }
    }
}