using AccessControl.DTOs;
using AccessControl.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;

namespace AccessControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessVisitorController : CommonController<AccessVisitor, AccessVisitorDto, AccessVisitorInsertDto, AccessVisitorUpdateDto>
    {
        public AccessVisitorController(ICommonService<AccessVisitorDto, AccessVisitorInsertDto, AccessVisitorUpdateDto> service)
            : base(service) { }
    }
}
