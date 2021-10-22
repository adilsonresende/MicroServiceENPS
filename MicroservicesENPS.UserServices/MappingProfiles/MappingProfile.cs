using AutoMapper;
using MicroserviceENPS.UserServices.DTOs;
using MicroserviceENPS.UserServices.Entities;

namespace MicroserviceENPS.UserServices.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserToInsertDTO>().ReverseMap();
        }
    }
}