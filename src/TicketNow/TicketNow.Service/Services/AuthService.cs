using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TicketNow.Domain.Dtos.Auth;
using TicketNow.Domain.Dtos.Default;
using TicketNow.Domain.Entities;
using TicketNow.Domain.Interfaces.Services;
using TicketNow.Domain.Utilities;
using TicketNow.Service.Validators.Auth;

namespace TicketNow.Service.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly UserManager<User> _userManager;        
        private readonly IConfiguration _configuration;        
        private readonly IMapper _mapper;

        public AuthService(UserManager<User> userManager,            
            IConfiguration configuration,            
            IMapper mapper)
        {
            _userManager = userManager;            
            _configuration = configuration;            
            _mapper = mapper;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginDto loginDto)
        {
            var validationResult = Validate(loginDto, Activator.CreateInstance<LoginValidator>());
            if (!validationResult.IsValid) { throw new ValidationException(validationResult.Errors); } //todo: add notification

            var user = await _userManager.FindByNameAsync(loginDto.Username);
            if (user is null || !user.Active) { throw new ValidationException("Credenciais invalidas!"); } //todo: add notification

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!isPasswordCorrect) { throw new ValidationException("Credenciais invalidas!"); } //todo: add notification

            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("JWTID", Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));

            var tokenObject = GenerateNewJsonWebToken(authClaims);

            return new LoginResponseDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(tokenObject),
                RefreshToken = string.Empty, //todo: implementar
                Expires = tokenObject.ValidTo,
            };
        }

        public async Task<DefaultServiceResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            var validationResult = Validate(registerDto, Activator.CreateInstance<RegisterValidator>());
            if (!validationResult.IsValid) { throw new ValidationException(validationResult.Errors); } //todo: add notification

            var existsUser = await _userManager.FindByNameAsync(registerDto.Username);
            if (existsUser is not null) { throw new ValidationException("Usuario já cadastrado"); } //todo: add notification

            var newUser = _mapper.Map<User>(registerDto);

            newUser.CreatedAt = DateTime.Now;
            newUser.Active = true;

            var createUserResult = await _userManager.CreateAsync(newUser, registerDto.Password);

            if (!createUserResult.Succeeded)            
                throw new Exception(string.Join(" ", createUserResult.Errors.Select(t => t.Code + " - " + t.Description)));  //todo: add notification                          

            await _userManager.AddToRoleAsync(newUser, StaticUserRoles.CUSTOMER);

            return new DefaultServiceResponseDto
            {
                Success = true,
                Message = "Usuario criado com sucesso!" //todo: melhorar isso
            };
        }

        private JwtSecurityToken GenerateNewJsonWebToken(List<Claim> claims)
        {
            var authSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            return new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(2),
                claims: claims,
                signingCredentials: new SigningCredentials(authSecret, SecurityAlgorithms.HmacSha256)
                );
        }
    }
}
