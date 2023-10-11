using TicketNow.Domain.Dtos.Auth;
using TicketNow.Domain.Dtos.Default;

namespace TicketNow.Domain.Interfaces.Services
{
    public interface IAuthService
    {
        Task<DefaultServiceResponseDto> RegisterAsync(RegisterDto registerDto);
        Task<LoginResponseDto> LoginAsync(LoginDto loginDto);        
    }
}
