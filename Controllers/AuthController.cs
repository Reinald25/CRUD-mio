using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CRUD.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        public class LoginModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginModel model)
        {
            // credenciales simples para testing
            if (model == null || model.Username != "admin" || model.Password != "admin")
            {
                return Unauthorized(new { message = "Credenciales inválidas" });
            }

            var jwtKey = _config["Jwt:Key"] ?? "CHANGE_ME_LONG_SECRET_KEY_12345";
            var jwtIssuer = _config["Jwt:Issuer"] ?? "MyIssuer";
            var jwtAudience = _config["Jwt:Audience"] ?? "MyAudience";

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(jwtKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, model.Username),
                    new Claim(ClaimTypes.Role, "Admin")
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                Issuer = jwtIssuer,
                Audience = jwtAudience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { token = tokenString, expires = tokenDescriptor.Expires });
        }
    }
}
