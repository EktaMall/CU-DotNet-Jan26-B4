using AutoMapper;
using NorthwindCatalog.Services.DTOs;
using NorthwindCatalog.Services.Models;

namespace NorthwindCatalog.Services.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>()
     .ForMember(dest => dest.ImageUrl,
         opt => opt.MapFrom(src => "/images/categories/" +
             src.CategoryName.ToLower()
                 .Replace("/", "")
                 .Replace(" ", "") + ".jpg"));

            CreateMap<Product, ProductDto>();
        }
    }
}
