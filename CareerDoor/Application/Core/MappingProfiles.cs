using Application.GetTogethers;
using AutoMapper;
using Domain;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<GetTogether, GetTogether>();
            CreateMap<GetTogether, GetTogetherDTO>();
        }
    }
}
