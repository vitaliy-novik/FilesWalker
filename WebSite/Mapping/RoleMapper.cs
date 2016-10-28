using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Infrastructure.Entities;
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
            Mapper.Initialize(cfg => cfg.CreateMap<RoleViewModel, Role>());
        }

        public static Role Map(RoleViewModel viewModel)
        {
            return new Role()
            {
                Id = viewModel.Id,
                Name = viewModel.Name
            };
        }

        public static RoleViewModel Map(IRole role)
        {
            RoleViewModel viewModel = new RoleViewModel()
            {
                Id = role.Id,
                Name = role.Name
            };

            return viewModel;
        }

        public static IEnumerable<Role> GetRoles(IEnumerable<RoleEditorViewModel> viewModel)
        {
            return viewModel.Where(role => role.UserInRole).Select(role => Map(role.Role));
        }
    }
}