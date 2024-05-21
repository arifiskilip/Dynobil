using AutoMapper;
using Entities.Concrete;
using WebUI.Models;

namespace WebUI.Profiles
{
    public class ProductProfile : Profile 
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductAddModel>().ReverseMap();
            CreateMap<Product, ProductUpdateModel>().ReverseMap();
        }
    }
}
