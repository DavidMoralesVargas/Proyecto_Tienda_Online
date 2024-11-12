using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tienda_Online.Backend.Clases;
using Tienda_Online.Shared.DTOs;
using Tienda_Online.Shared.Entidades;

namespace Tienda_Online.Backend.Controllers
{
    [ApiController]
    [Route("/api/cuentas")]
    public class AccountsController:ControllerBase
    {
        private readonly clsUsuario _usuarios;
        private readonly IConfiguration _configuration;

        public AccountsController(clsUsuario usuarios, IConfiguration configuration)
        {
            _usuarios = usuarios;
            _configuration = configuration;
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody]UserDTO model)
        {
            Usuario user = model;
            var result = await _usuarios.AddUserAsync(user, model.Password);
            if(result.Succeeded)
            {
                await _usuarios.AddUserToRoleAsync(user, user.userType.ToString());
                return Ok(BuildToken(user));
            }
            return BadRequest(result.Errors.FirstOrDefault());
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody]LoginDTO model)
        {
            var result = await _usuarios.LoginAsync(model);
            if (result.Succeeded)
            {
                var user = await _usuarios.GetUserAsync(model.Email);
                return Ok(BuildToken(user));
            }
            return BadRequest("Email o contraseña incorrectos");
        }

        private TokenDTO BuildToken(Usuario user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.Email!),
                new(ClaimTypes.Role, user.userType.ToString()),
                new("Nombre", user.Nombre),
                new("Apellido", user.Apellido),
                new("Direccion", user.Direccion),
                new("FechaNacimiento", user.FechaNacimiento.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwtKey"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddDays(30);
            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: credentials);
            return new TokenDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
