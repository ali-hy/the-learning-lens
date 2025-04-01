using AutoMapper;
using the_learning_lens.Dtos.Module;
using the_learning_lens.Dtos.UserAccount;
using the_learning_lens.Models;

namespace the_learning_lens.Helpers
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
