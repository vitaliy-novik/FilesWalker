using Business.Core.Services;
using Business.Interface.Services;
using EFRepositories.Repositories;
using Microsoft.Practices.Unity;
using Repository.Interface.Repositories;

namespace CompositionRoot
{
    public static class IocBuilder
    {
        public static void BuildContainer(UnityContainer builder)
        {
            ResolveRepositoryDependencies(builder);
            ResolveServiceDependencies(builder);
        }

        private static void ResolveRepositoryDependencies(UnityContainer builder)
        {
            builder.RegisterType<IUserRepository, UserRepository>();
            builder.RegisterType<IRoleRepository, RoleRepository>();
        }

        private static void ResolveServiceDependencies(UnityContainer builder)
        {
            builder.RegisterType<IUserService, UserService>();
            builder.RegisterType<IRoleService, RoleService>();
            builder.RegisterType<IFoldersService, FoldersService>();
        }
    }
}
