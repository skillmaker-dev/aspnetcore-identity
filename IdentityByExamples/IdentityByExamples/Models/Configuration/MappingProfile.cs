using AutoMapper;

namespace IdentityByExamples.Models.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegistrationModel,User>().ForMember(u => u.UserName,opt => opt.MapFrom(x => x.Email));
        }
    }
}
