using ApiFinal.App.Dtos.Categories;
using ApiFinal.App.Entities;
using AutoMapper;

namespace ApiFinal.App.Profiles.CategoriesMap
{
    public class CategoryMapProfile : Profile
    {
        public CategoryMapProfile()
        {
            CreateMap<CategoryPostDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();
        }
    }
}
