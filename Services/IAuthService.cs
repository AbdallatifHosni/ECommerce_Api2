using System.Threading.Tasks;
using ECommerce_Api2.Dtos;
namespace ECommerce_Api2.Services;

public interface IAuthService
{
    Task<AuthDto> RegisterAsync(RegesterDto model);
    Task<AuthDto> LoginAsync(LoginDto model);
    Task<string> AddRoleAsync(AddRoleDto model);
}
