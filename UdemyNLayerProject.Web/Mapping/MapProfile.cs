using AutoMapper;
using UdemyNLayerProject.Core.Models;
using UdemyNLayerProject.Web.DTOS;

namespace UdemyNLayerProject.Web.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();

            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();

            CreateMap<Category, CategoryWithProductDto>();
            CreateMap<CategoryWithProductDto, Category>();

            CreateMap<ProductWithCategoryDto, Product>();
            CreateMap<Product, ProductWithCategoryDto>();

            CreateMap<PersonDto, Person>();
            CreateMap<Person, PersonDto>();
        }
    }
}
