using AutoMapper;
using Users.Dtos;
using Users.Models;

namespace Users.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            //Source -> Target
            CreateMap<UserCreateDto, User>();
        }
    }
}