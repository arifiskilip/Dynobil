using Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(RegisterDto registerDto);
    }
}
