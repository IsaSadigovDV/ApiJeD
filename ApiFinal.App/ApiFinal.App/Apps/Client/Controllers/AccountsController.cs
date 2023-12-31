using ApiFinal.Service.Dtos.Accounts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApiFinal.App.Apps.Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
         private readonly UserManager<IdentityUser> _userManager;
         private readonly RoleManager<IdentityRole> _roleManager;
        public AccountsController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterDto dto)
        {
            IdentityUser identityUser = new IdentityUser
            {
                Email= dto.Email,
                UserName=dto.Username,
            };

            var result  = await _userManager.CreateAsync(identityUser, dto.Password);
            if(!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            await _userManager.AddToRoleAsync(identityUser, "Admin");
            return Ok();
        }
        //[HttpPost]
        //public async Task<IActionResult> CreateRole()
        //{
        //    await _roleManager.CreateAsync(new IdentityRole { Name = "SuperAdmin" });
        //    await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
        //    await _roleManager.CreateAsync(new IdentityRole { Name = "Member" });
        //    return Ok();
        //}
    }
}
