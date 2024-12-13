using AssetManagementSystem.Models;
using AssetManagementSystem.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ILoginRepository _loginRepository;

        // Dependency Injection
        public LoginsController(IConfiguration config, ILoginRepository loginRepository)
        {
            _config = config;
            _loginRepository = loginRepository;
        }

        #region --validate username and password
        // GET api/Logins/username/password
        [HttpGet("{username}/{password}")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginCredentials(string username, string password)
        {
            // Variable for tracking unauthorized
            IActionResult response = Unauthorized(); // 401
            Login validUser = null;

            // 1 - Authenticate the user by passing username and password
            validUser = await _loginRepository.ValidateUser(username, password);

            // 2 - Generate JWT Token
            if (validUser != null)
            {
                // Custom Method for generating token
                var tokenString = GenerateJWTToken(validUser);

                response = Ok(new
                {
                    uName = validUser.Username,
                    roleId = validUser.RoleId,
                    token = tokenString
                });
            }
            return response;
        }
        #endregion

        #region Generate JWT Token
        private string GenerateJWTToken(Login validUser)
        {
            // 1 - Secret key
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            // 2 - Algorithm
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            // 3 - JWT
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                null,
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: credentials);

            // 4 - Writing Token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion
    }
}
