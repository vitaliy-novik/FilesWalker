using AutoMapper;
using WebSite.Models;
using WebSite.ViewModels.Account;

namespace WebSite.Mapping
{
    public static class UserMapper
    {
        static UserMapper()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<IdentityUser, RegistrationViewModel>());
            Mapper.Initialize(cfg => cfg.CreateMap<RegistrationViewModel, IdentityUser>());
        }

        public static IdentityUser Map(RegistrationViewModel viewModelUser)
        {
            return Mapper.Map<RegistrationViewModel, IdentityUser>(viewModelUser);
        }

        public static RegistrationViewModel Map(IdentityUser identityUser)
        {
            return Mapper.Map<IdentityUser, RegistrationViewModel>(identityUser);
        }
    }
}