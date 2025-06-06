using AutoMapper;
using Integration.Dtos.Module;
using Integration.Dtos.UserAccount;
using WebApp.Models;

namespace WebApp.Helpers
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterRequest, UserAccount>()
                .ForMember(o => o.UserName, o => o.MapFrom(r => r.Email));
            CreateMap<UserAccount, UserAccountFlatResponse>();

            CreateMap<CreateModuleRequest, Module>();
        }
    }
}
