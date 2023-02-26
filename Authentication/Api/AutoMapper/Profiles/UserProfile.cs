using AutoMapper;
using Models.DTOs.Requests;
using Models.Entities.Identity;

namespace Api.AutoMapper.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRegistrationRequestDTO, User>()
                .ForMember(
                    dest => dest.PasswordHash,
                    options => options.MapFrom(src => src.Password))
                .ForMember(
                    dest => dest.UserName,
                    options => options.MapFrom(src => src.Email));
        }

    }
}
