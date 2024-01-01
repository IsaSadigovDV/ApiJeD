using ApiFinal.Service.Dtos.Accounts;
using ApiFinal.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiFinal.Service.Services.Interfaces
{
    public interface IIdentityService
    {
        public Task<ApiResponse> Register(RegisterDto dto);
        public Task<ApiResponse> Login(LoginDto dto);
    }
}
