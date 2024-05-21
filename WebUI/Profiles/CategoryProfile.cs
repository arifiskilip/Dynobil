using AutoMapper;
using Entities.Concrete;
using WebUI.Models;

namespace WebUI.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryAddModel>().ReverseMap();
            CreateMap<Category, CategoryUpdateModel>().ReverseMap();

        }
    }
}
