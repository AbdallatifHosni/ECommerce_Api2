using ECommerce_Api2.Services;
using ECommerce_Api2.Models;
using ECommerce_Api2.Data;
using ECommerce_Api2.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AngEcommerceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public AccountController(IAuthService authService,
            UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _authService = authService;
            _userManager = userManager;
            this.roleManager = roleManager;
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegesterDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _authService.RegisterAsync(model);
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> GetTokenAsync([FromBody] LoginDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.LoginAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }
        [HttpPatch("UpdateUser")]
        public async Task<IActionResult>UpdateUser(string id , [FromBody] RegesterDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return BadRequest();
            var resEm = await _userManager.FindByEmailAsync(model.Email);
            if (resEm != null) 
            {
                if(model.Email != user.Email)
                    return BadRequest();
            }
            resEm = await _userManager.FindByNameAsync(model.UserName);
            if (resEm != null)
            {
                if (model.UserName != user.UserName)
                    return BadRequest();
            }
            var resultPass = await _userManager.ChangePasswordAsync(user,user.PasswordHash,model.Password);
            if (resultPass.Succeeded)
            {
                user.Password = model.Password;
                user.Name = model.Name;
                user.UserName = model.UserName;
                user.Email = model.Email;
                user.City = model.City;
                user.PostalCode = model.PostalCode;
                user.Street = model.Street;
                user.PhoneNumber = model.PhoneNumber;
                user.Password = model.Password;
                var result = await _userManager.UpdateAsync(user);
                return Ok(result);
            }
            return BadRequest(resultPass.Errors);
         }
        [HttpGet("filter")]
        public async Task<IActionResult> GetFiltered([FromQuery] FilteredProduct product)
        {
            var result = await _userManager.Users.ToListAsync();
            var users = await usersRolles(result);
            users = users.Skip(product.Page * product.Size).Take(product.Size).ToList();
            if (product.Filter != null)
            {
                users = users.FindAll(x => (x.Name.Any(i => product.Filter.Contains(i))));
            }
            if (product.Order != "asc")
            {
                users.Reverse();
            }
            if (users != null)
            {
                return Ok(users);
            }
            return BadRequest();
        }
       
        [HttpPost("addrole")]
        public async Task<IActionResult> AddRoleAsync([FromBody] AddRoleDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.AddRoleAsync(model);

            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);

            return Ok(model);
        }
        [HttpGet("check")]
        public async Task<IActionResult> checker(string UsId=null,string email=null , string username=null )
        {
            if (email != null)
            {
                email = email.Trim();
                var res = await _userManager.FindByEmailAsync(email);
                
                if (UsId != null)
                {
                    var user = await _userManager.FindByIdAsync(UsId);
                    if (res != null && user != null)
                    {
                        if (email != user.Email)
                            return Ok(true);
                    }
                }
                
                if (res is null)
                    return Ok(false);

                return Ok(true);
            }
            if (username != null)
            {
                var resUs = await _userManager.FindByNameAsync(username);
                if(UsId != null)
                {
                    var user = await _userManager.FindByIdAsync(UsId);
                    if (resUs != null && user != null)
                    {
                        if (username != user.UserName)
                            return Ok(false);
                    }
                }
                if (resUs is null)
                    return Ok(false);
                return Ok(true);
            }
            return BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> getAllUsers()
        {
            var result = await _userManager.Users.ToListAsync();
            return Ok(result);
        }
        private async Task<List<UserRollesDto>> usersRolles(List<User> list)
        {
            List<UserRollesDto> res = new();
            foreach (var item in list)
            {

                var result = await _userManager.GetRolesAsync(item);
                var user = new UserRollesDto();
                user.Name = item.Name;
                user.Id = item.Id;
                user.PhoneNumber = item.PhoneNumber;
                user.PostalCode = item.PostalCode;
                user.City = item.City;
                user.Street = item.Street;
                user.Email = item.Email;
                user.UserName = item.UserName;
                user.Roles = result.ToList();
                res.Add(user);
            }
            return res;
        }

    }
}
