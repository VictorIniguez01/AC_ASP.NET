using AccessControl.DTOs;
using AccessControl.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AccessControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private ISessionService<UserAcDto> _loginService;
        private IConfiguration _configuration;
        public SessionController(IConfiguration configuration,
                               ISessionService<UserAcDto> loginService)
        {
            _configuration = configuration;
            _loginService = loginService;
        }

        [HttpPost("Signin")]
        public async Task<IActionResult> Signin(UserAcDto userAcDto)
        {
            UserAcDto user = await _loginService.Auth(userAcDto);
            if (user == null)
                return NotFound();

            var jwt = _configuration.GetSection("Jwt");
            var tokenHandler = new JwtSecurityTokenHandler();
            var byteKey = Encoding.UTF8.GetBytes(jwt["Key"]);
            var tokenDes = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("id", user.UserAcId.ToString()),
                    new Claim(ClaimTypes.Role, user.UserAcIsZone == true ? "UserZone" : "UserResidential"),
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(byteKey),
                                                                SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDes);

            return Ok(new { Token = tokenHandler.WriteToken(token)  });
        }
    }
}
