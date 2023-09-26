using AuthJwt.Constants;
using AuthJwt.Models;
using AuthJwt.Models.Data;
using AuthJwt.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly AuthJwtContext _context;

        public LoginController(IConfiguration config, AuthJwtContext context)
        {
            _config = config;
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var currentUser = GetCurrentUser();
            return Ok($"Hola {currentUser.FirstName}, Rol: {currentUser.Rol}");
        }

        private UserModel GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null) 
            {
                var UserClaims = identity.Claims;

                return new UserModel
                {
                    UserName = UserClaims.FirstOrDefault(p => p.Type == ClaimTypes.NameIdentifier)?.Value,
                    FirstName = UserClaims.FirstOrDefault(p => p.Type == ClaimTypes.GivenName)?.Value,
                    Rol  = UserClaims.FirstOrDefault(p => p.Type == ClaimTypes.Role)?.Value
                };
            }
            return null!;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserDTO loginUserDTO)
        {
            var user = await Authenticate(loginUserDTO);

            if (user != null)
            {
                var token = GenerateToken(user);

                return Ok(token);
            }
            return NotFound("User Not Found");
        }

        private string GenerateToken(User user)
        {
            //Crear los claims
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
             {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                //new Claim(ClaimTypes.Email, user.Email),
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

        private async Task<User> Authenticate(LoginUserDTO loginUserDTO)
        {
            //var currentUser = UserConstants.Users.FirstOrDefault(user => user.UserName.ToLower() == loginUserDTO.UserName.ToLower() &&
            //                                                             user.Password == loginUserDTO.Password);
            var currentUser = await _context.Users.FirstOrDefaultAsync(user => user.UserName.ToLower() == loginUserDTO.UserName.ToLower() &&
                                                                                user.Password == loginUserDTO.Password);
            if(currentUser != null) 
            {
                return currentUser;
            }

            return null!;
        }

    }
}
