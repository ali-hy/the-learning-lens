using AutoMapper;
using WebApp.Dtos.Module;
using WebApp.Dtos.UserAccount;
using WebApp.Models;

namespace WebApp.Helpers
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterRequest, UserAccount>()
                .ForMember(o => o.UserName, o => o.MapFrom(r => r.Email));

            CreateMap<CreateModuleRequest, Module>();
        }
    }
}
