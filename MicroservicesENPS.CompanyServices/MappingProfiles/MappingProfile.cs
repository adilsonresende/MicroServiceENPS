using AutoMapper;
using MicroservicesENPS.CompanyServices.DTOs;
using MicroservicesENPS.CompanyServices.Entities;

namespace MicroservicesENPS.CompanyServices.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Company, CompanyDTO>().ReverseMap();
        }
    }
}