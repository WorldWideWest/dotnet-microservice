using AutoMapper;
using Models.DTOs.Requests;
using Models.Entities.Identity;

namespace Api.AutoMapper.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRegistrationRequestDTO, User>();
        }

    }
}
