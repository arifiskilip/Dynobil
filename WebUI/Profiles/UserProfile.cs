using AutoMapper;
using Entities.Concrete;
using WebUI.Models;

namespace WebUI.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, LoginModel>().ReverseMap();
            CreateMap<User, RegisterModel>().ReverseMap();
        }
    }
}
