using AutoMapper;
using DevaloreAssignment.Dto;
using DevaloreAssignment.Models;

namespace DevaloreAssignment.Helper
{
    public class MappingProfile : Profile // automapper couldnt be imported
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<CreateUserDto, User>();
            CreateMap<User, CreateUserDto>();
        }

    }
}
