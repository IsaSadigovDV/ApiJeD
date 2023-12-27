using ApiFinal.Core.Entities;
using ApiFinal.Core.Repositories.Interfaces;
using ApiFinal.Service.Dtos.Categories;
using ApiFinal.Service.Responses;
using ApiFinal.Service.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiFinal.Service.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _repository;

        public CategoryService(IMapper mapper, ICategoryRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ApiResponse> CreateAsync(CategoryPostDto dto)
        {
            if(await _repository.IsExsist(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower()))
            {
                return new ApiResponse { StatusCode = 400, Description = "Name already exsist" };
            }

            Category category = _mapper.Map<Category>(dto);
            await _repository.AddAsync(category);
            await _repository.SaveAsync();
            return new ApiResponse { StatusCode = 201, Items=category};
        }

        public async Task<ApiResponse> GetAllAsync()
        {
            IQueryable<Category> query = await _repository.GetAllAsync(x => !x.IsDeleted);

            List<CategoryGetDto> categories = new List<CategoryGetDto>();

            categories = await query.Select(x => new CategoryGetDto { Name = x.Name }).ToListAsync();
        
            return new ApiResponse { Items = categories, StatusCode = 200};
        }

        public async Task<ApiResponse> GetAsync(int id)
        {
            Category? category = await _repository.GetAsync(x => x.Id == id && !x.IsDeleted);

            if (category == null)
            {
                return new ApiResponse { StatusCode = 404, Description = "Item not found" };
            }

            CategoryGetDto getDto = _mapper.Map<CategoryGetDto>(category);

            return new ApiResponse { Items = getDto, StatusCode = 200 };
        }

        public async Task<ApiResponse> RemoveAsync(int id)
        {
            Category? category = await _repository.GetAsync(x => x.Id == id && !x.IsDeleted);

            if (category == null)
            {
                return new ApiResponse { StatusCode = 404, Description = "Item not found" };
            }

            category.IsDeleted = true;
            await _repository.Update(category);
            await _repository.SaveAsync();
            return new ApiResponse { StatusCode=200};
        }

        public async Task<ApiResponse> UpdateAsync(int id , CategoryUpdateDto dto)
        {
            Category? category = await _repository.GetAsync(x => x.Id == id && !x.IsDeleted);

            if (category == null)
            {
                return new ApiResponse { StatusCode = 404, Description = "Item not found" };
            }

            category.Name = dto.Name;
            await _repository.Update(category);
            await _repository.SaveAsync();
            return new ApiResponse { StatusCode = 200 };
        }
    }
}
