using AutoMapper;
using Resources.Dtos;
using Resources.Models;

namespace Resources.Profiles
{
    public class ResourcesProfile : Profile
    {
        public ResourcesProfile()
        {    
            //Source -> Target
            CreateMap<Resource,ResourceReadDto>();
            CreateMap<ResourceCreateDto,Resource>();
            CreateMap<ResourceUpdateDto,Resource>();
            CreateMap<Resource,ResourceUpdateDto>();
        }
    }
}