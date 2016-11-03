using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using AutoMapper;
using Business.Interface.Services;
using Infrastructure.Entities;
using WebSite.Models;
using WebSite.ViewModels.Account;
using WebSite.ViewModels.Roles;
using WebSite.ViewModels.Users;

namespace WebSite.Mapping
{
    public static class UserMapper
    {
        static UserMapper()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<IdentityUser, RegistrationViewModel>());
            Mapper.Initialize(cfg => cfg.CreateMap<RegistrationViewModel, IdentityUser>());
            Mapper.Initialize(cfg => cfg.CreateMap<EditUserViewModel, IdentityUser>());
        }

        public static IdentityUser Map(RegistrationViewModel viewModelUser)
        {
            return new IdentityUser()
            {
                UserName = viewModelUser.UserName,
                Email = viewModelUser.Email,
                Password = viewModelUser.Password
            };
        }

        public static IdentityUser MapUserToIdentity(IUser user)
        {
            return new IdentityUser(user.Id)
            {
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password
            };
        }

        public static RegistrationViewModel Map(IdentityUser identityUser)
        {
            return Mapper.Map<IdentityUser, RegistrationViewModel>(identityUser);
        }

        public static UserViewModel Map(IUser user, IRoleService roleService)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<IUser, UserViewModel>()
                .ForMember("Roles", opt => opt.MapFrom(u => roleService.GetRoles(u))));
            UserViewModel viewModel = Mapper.Map<IUser, UserViewModel>(user);
            return viewModel;
        }

        public static EditUserViewModel MapToEditUserViewModel(IUser user, IRoleService roleService)
        {
            IEnumerable<IRole> allRoles = roleService.GetAllRoles();
            Mapper.Initialize(cfg => cfg.CreateMap<IUser, EditUserViewModel>()
                .ForMember("UserInRoles", opt => opt.MapFrom(u => allRoles.Select(role => new RoleEditorViewModel()
                {
                    Role = RoleMapper.Map(role),
                    UserInRole = roleService.GetRoles(user).Any(r => r.Id == role.Id)
                }))));

            EditUserViewModel viewModel = Mapper.Map<IUser, EditUserViewModel>(user);
            return viewModel;
        }

        public static IdentityUser Map(EditUserViewModel viewModel)
        {
            return new IdentityUser()
            {
                Id = viewModel.Id,
                UserName = viewModel.UserName,
                Password = viewModel.Password,
                Email = viewModel.Email
            };
        }


    }
}