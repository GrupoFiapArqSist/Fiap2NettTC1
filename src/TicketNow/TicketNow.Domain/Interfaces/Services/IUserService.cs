using Microsoft.AspNetCore.Http;
using TicketNow.Domain.Dtos.Default;
using TicketNow.Domain.Dtos.User;

namespace TicketNow.Domain.Interfaces.Services
{
    public interface IUserService
    {
        IList<UserResponseDto> GetAll();
        UserResponseDto GetById(int id);
        Task<DefaultServiceResponseDto> Update(UpdateUserDto updateUserDto, int id);
        Task<DefaultServiceResponseDto> UpdatePassword(UpdateUserPasswordDto updateUserPasswordDto, int id);
        Task<DefaultServiceResponseDto> UploadPhoto(IFormFile file, int id);

    }
}
