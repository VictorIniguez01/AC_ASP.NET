using AccessControl.DTOs;
using AccessControl.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AccessControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IReadService<UserAcDto> _readService;
        private ILoginService<UserAcDto> _loginService;
        private IConfiguration _configuration;
        public LoginController(IReadService<UserAcDto> readService,
                               IConfiguration configuration,
                               ILoginService<UserAcDto> loginService)
        {
            _readService = readService;
            _configuration = configuration;
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserAcDto userAcDto)
        {
            UserAcDto user = await _loginService.Auth(userAcDto);
            if (user == null)
                return NotFound();

            var jwt = _configuration.GetSection("Jwt");
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, jwt["Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("id", user.UserAcId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]));
            var signin = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(jwt["Issuer"], jwt["Audience"], claims, expires: DateTime.Now.AddMinutes(60), signingCredentials: signin);
            return Ok(new 
            {
                success = true,
                message = "Successed",
                result = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }
    }
}
