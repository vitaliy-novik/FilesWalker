using Business.Core.Services;
using Business.Interface.Services;
using EFRepositories.Repositories;
using Microsoft.Practices.Unity;
using Repository.Interface.Repositories;

namespace CompositionRoot
{
    /// <summary>
    /// Class to register all BLL and DAL dependencies
    /// </summary>
    public static class IocBuilder
    {
        public static void BuildContainer(UnityContainer builder)
        {
            ResolveRepositoryDependencies(builder);
            ResolveServiceDependencies(builder);
        }

        /// <summary>
        /// Regsters all DAL dependecies
        /// </summary>
        /// <param name="builder">Unity IoC container</param>
        private static void ResolveRepositoryDependencies(UnityContainer builder)
        {
            builder.RegisterType<IUserRepository, UserRepository>();
            builder.RegisterType<IRoleRepository, RoleRepository>();
        }


        /// <summary>
        /// Registers all BLL dependencies
        /// </summary>
        /// <param name="builder">Unity IoC container</param>
        private static void ResolveServiceDependencies(UnityContainer builder)
        {
            builder.RegisterType<IUserService, UserService>();
            builder.RegisterType<IRoleService, RoleService>();
            builder.RegisterType<IFoldersService, FoldersService>();
        }
    }
}
