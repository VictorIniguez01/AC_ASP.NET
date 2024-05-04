using AccessControl.DTOs;
using AccessControl.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;

namespace AccessControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessResidentController : CommonController<AccessResident, AccessResidentDto, AccessResidentInsertDto, AccessResidentUpdateDto>
    {
        public AccessResidentController(ICommonService<AccessResidentDto, AccessResidentInsertDto, AccessResidentUpdateDto> service)
            : base(service) { }
    }
}
