using ApiFinal.Core.Entities;
using ApiFinal.Core.Repositories.Interfaces;
using ApiFinal.Service.Dtos.Products;
using ApiFinal.Service.Responses;
using ApiFinal.Service.Services.Interfaces;
using ApiFinal.Service.Extentions;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace ApiFinal.Service.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _http;

        public ProductService(IProductRepository productRepository, IMapper mapper, IWebHostEnvironment env, ICategoryRepository categoryRepository, IHttpContextAccessor http)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _env = env;
            _categoryRepository = categoryRepository;
            _http = http;
        }

        public async Task<ApiResponse> CreateAsync(ProductPostDto dto)
        {
            if (! await _categoryRepository.IsExsist(x => x.Id == dto.CategoryId))
            {
                return new ApiResponse { StatusCode = 204, Description = "Category id is invalid" };
            }

            Product product = _mapper.Map<Product>(dto);
            product.Image = dto.File.SaveFile(_env.WebRootPath, "assets/images");
            product.ImageUrl = _http.HttpContext.Request.Scheme + "://" + _http.HttpContext.Request.Host + $"/assets/images/{product.Image}";
            await _productRepository.AddAsync(product);
            await _productRepository.SaveAsync();
            return new ApiResponse { StatusCode = 201 };
        }

        public async Task<ApiResponse> GetAllAsync()
        {
            var query = await _productRepository.GetAllAsync(x => !x.IsDeleted, "Category");

            IEnumerable<ProductGetDto> productGetDtos = await
                query.Select(x => new ProductGetDto
                {
                    Image = x.Image,
                    ImageUrl = x.ImageUrl,
                    CategoryId = x.CategoryId,
                    Name = x.Name,
                    Price = x.Price,
                    CategoryName = x.Category.Name
                }).ToListAsync();

            return new ApiResponse { StatusCode = 200, Items = productGetDtos };
        }

        public async Task<ApiResponse> GetAsync(int id)
        {
            Product product = await _productRepository.GetAsync(x => !x.IsDeleted && x.Id == id, "Category");

            if (product == null)
            {
                return new ApiResponse { StatusCode = 404, Description = "Not Found" };
            }
            ProductGetDto productGetDto = _mapper.Map<ProductGetDto>(product);
            productGetDto.CategoryName = product.Category.Name;

            return new ApiResponse { StatusCode = 200, Items = productGetDto };
        }

        public async Task<ApiResponse> RemoveAsync(int id)
        {
            Product product = await _productRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (product == null)
            {
                return new ApiResponse { StatusCode = 404, Description = "Not Found" };
            }

            product.IsDeleted = true;
            await _productRepository.Update(product);
            await _productRepository.SaveAsync();
            return new ApiResponse { StatusCode = 204 };
        }

        public async Task<ApiResponse> UpdateAsync(int id, ProductUpdateDto dto)
        {
            Product product = await _productRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (product == null)
            {
                return new ApiResponse { StatusCode = 404, Description = "Not Found" };
            }

            product.Name = dto.Name;
            product.Price = dto.Price;
            product.CategoryId = dto.CategoryId;
            product.Image = dto.File == null ? product.Image : dto.File.SaveFile(_env.WebRootPath, "assets/images");
            await _productRepository.Update(product);
            await _productRepository.SaveAsync();
            return new ApiResponse { StatusCode = 204 };
        }
    }
}
