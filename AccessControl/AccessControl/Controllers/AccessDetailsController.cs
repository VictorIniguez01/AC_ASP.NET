using AccessControl.DTOs;
using AccessControl.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AccessControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessDetailsController : ControllerBase
    {
        private IConfiguration _configuration;
        private IUserAcService<DeviceDto, VisitorDto, CarDto, AccessVisitorDto, AccessDetailsDto> _userAcService;
        public AccessDetailsController(IConfiguration configuration,
                                       IUserAcService<DeviceDto, VisitorDto, CarDto, AccessVisitorDto, AccessDetailsDto> userAcService)
        {
            _configuration = configuration;
            _userAcService = userAcService;
        }

        [HttpPost]
        public async Task<IActionResult> GetAccessDetails()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var idClaim = identity.Claims.FirstOrDefault(x => x.Type == "id");

            if (idClaim == null)
                return NotFound();

            return Ok(await _userAcService.GetAccessDetails(Int32.Parse(idClaim.Value)));
        }
    }
}
