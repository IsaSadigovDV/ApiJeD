using ApiFinal.Service.Dtos.Products;
using ApiFinal.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiFinal.Service.Services.Interfaces
{
    public interface IProductService
    {
        public Task<ApiResponse> CreateAsync(ProductPostDto dto);
        public Task<ApiResponse> UpdateAsync(int id, ProductUpdateDto dto);
        public Task<ApiResponse> RemoveAsync(int id);
        public Task<ApiResponse> GetAsync(int id);
        public Task<ApiResponse> GetAllAsync();
    }
}
