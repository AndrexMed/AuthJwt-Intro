using AuthJwt.Constants;
using AuthJwt.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthJwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        public IActionResult Login(LoginUserDTO loginUserDTO)
        {
            var user = Authenticate(loginUserDTO);

            if (user != null)
            {
                var token = GenerateToken(user);

                return Ok(token);
            }
            return NotFound("User Not Found");
        }

        private string GenerateToken(UserModel user)
        {
            //Crear los claims
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
         {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Role, user.Rol),
            };
            //Crear el token
                var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserModel Authenticate(LoginUserDTO loginUserDTO)
        {
            var currentUser = UserConstants.Users.FirstOrDefault(user => user.UserName.ToLower() == loginUserDTO.UserName.ToLower() &&
                                                                         user.Password == loginUserDTO.Password);
            if(currentUser != null) 
            {
                return currentUser;
            }

            return null;
        }
    }
}
