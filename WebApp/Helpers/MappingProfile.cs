using AutoMapper;
using Integration.Dtos.Lesson;
using Integration.Dtos.Prefab;
using Integration.Dtos.UserAccount;
using WebApp.Forms;
using WebApp.Models;

namespace WebApp.Helpers
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            // User
            CreateMap<RegisterRequest, UserAccount>()
                .ForMember(o => o.UserName, o => o.MapFrom(r => r.Email));
            CreateMap<UserAccount, UserAccountFlatResponse>();

            // ArLesson
            CreateMap<CreateArLessonForm, ArLesson>()
                .ForMember(o => o.Preview, o => o.Ignore())
                .ForMember(o => o.ReferenceImages, o => o.Ignore());

            // Lesson
            CreateMap<CreateLessonForm, Lesson>()
                .ForMember(o => o.Preview, o => o.Ignore())
                .ForMember(o => o.Prefab, o => o.Ignore());

            CreateMap<Lesson, LessonBase>();
            CreateProjection<Lesson, LessonBase>();

            // Prefab
            CreateMap<Prefab, PrefabBase>();
        }
    }
}
