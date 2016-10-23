using AutoMapper;
using WebSite.ViewModels.Roles;
using IRole = Infrastructure.Entities.IRole;

namespace WebSite.Mapping
{
    public static class RoleMapper
    {
        static RoleMapper()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<IRole, RoleViewModel>()
                .ForMember("Id", opt => opt.MapFrom(role => role.Id)));
            Mapper.Initialize(cfg => cfg.CreateMap<RoleViewModel, IRole>());
        }

        public static IRole Map(RoleViewModel viewModel)
        {
            return Mapper.Map<RoleViewModel, IRole>(viewModel);
        }

        public static RoleViewModel Map(IRole role)
        {
            RoleViewModel viewModel = new RoleViewModel()
            {
                Id = role.Id,
                Name = role.Name
            };

            return viewModel;
            //Mapper.Map<IRole, RoleViewModel>(role);
        } 
    }
}