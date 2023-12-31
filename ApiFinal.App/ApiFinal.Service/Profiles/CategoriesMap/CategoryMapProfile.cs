using ApiFinal.Service.Dtos.Categories;
using ApiFinal.Core.Entities;
using AutoMapper;

namespace ApiFinal.Service.Profiles.CategoriesMap
{
    public class CategoryMapProfile : Profile
    {
        public CategoryMapProfile()
        {
            CreateMap<CategoryPostDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();
            CreateMap<Category, CategoryGetDto>();
        }
    }
}
