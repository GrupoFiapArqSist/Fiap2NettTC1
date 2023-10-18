using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using TicketNow.Domain.Dtos.Default;
using TicketNow.Domain.Dtos.User;
using TicketNow.Domain.Extensions;
using TicketNow.Domain.Interfaces.Services;
using TicketNow.Domain.Utilities;

namespace TicketNow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all users")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IList<UserResponseDto>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get user by id")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(UserResponseDto))]
        [SwaggerResponse((int)HttpStatusCode.NoContent)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(IReadOnlyCollection<dynamic>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult GetById(int id)
        {
            var user = _userService.GetById(id);
            return Ok(user);
        }

        [HttpPut]
        [Authorize(Roles = StaticUserRoles.CUSTOMER)]
        [SwaggerOperation(Summary = "Update user")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(DefaultServiceResponseDto))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(IReadOnlyCollection<dynamic>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById([FromBody] UpdateUserDto updateUserDto)
        {
            var response = await _userService.Update(updateUserDto, this.GetUserIdLogged());
            return Ok(response);
        }

        [HttpPost("ChangePassword")]
        [Authorize(Roles = StaticUserRoles.CUSTOMER)]
        [SwaggerOperation(Summary = "Change password")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(DefaultServiceResponseDto))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(IReadOnlyCollection<dynamic>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ChangePassword([FromBody] UpdateUserPasswordDto updateUserPasswordDto)
        {
            var response = await _userService.UpdatePassword(updateUserPasswordDto, this.GetUserIdLogged());
            return Ok(response);
        }

        [HttpPost("UploadPhoto")]
        [Authorize(Roles = StaticUserRoles.CUSTOMER)]
        [SwaggerOperation(Summary = "Upload Photo")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(DefaultServiceResponseDto))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(IReadOnlyCollection<dynamic>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UploadPhoto(IFormFile file)
        {
            var response = await _userService.UploadPhoto(file, this.GetUserIdLogged());
            return Ok(response);
        }
    }
}
