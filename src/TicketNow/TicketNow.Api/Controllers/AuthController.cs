﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using TicketNow.Domain.Dtos.Auth;
using TicketNow.Domain.Dtos.Default;
using TicketNow.Domain.Extensions;
using TicketNow.Domain.Interfaces.Services;
using TicketNow.Domain.Utilities;

namespace TicketNow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("register")]
        [SwaggerOperation(Summary = "Create a new user")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(DefaultServiceResponseDto))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(IReadOnlyCollection<dynamic>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var register = await _authService.RegisterAsync(registerDto);
            return Ok(register);
        }

        [HttpPost]
        [Route("login")]
        [SwaggerOperation(Summary = "Get user token")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(LoginResponseDto))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(IReadOnlyCollection<dynamic>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var login = await _authService.LoginAsync(loginDto);
            return Ok(login);
        }

        [HttpPost]
        [Route("refresh-token")]
        [Authorize]
        [SwaggerOperation(Summary = "Refresh user token")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(LoginResponseDto))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(IReadOnlyCollection<dynamic>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto refreshTokenDto)
        {
            if (refreshTokenDto is null)
                return BadRequest("Parametros invalidos!");

            var refresh = await _authService.RefreshTokenAsync(this.GetAccessToken(), refreshTokenDto.RefreshToken, User.Identity.Name);
            return Ok(refresh);
        }

        [HttpPost]
        [Route("revoke/{username}")]
        [Authorize(Roles = StaticUserRoles.ADMIN)]
        [SwaggerOperation(Summary = "Revoke user token")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(DefaultServiceResponseDto))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(IReadOnlyCollection<dynamic>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Revoke(string username)
        {
            var revoke = await _authService.RevokeAsync(username);
            return Ok(revoke);
        }
    }
}
