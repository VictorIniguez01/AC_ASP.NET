using AccessControl.DTOs;
using AccessControl.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;

namespace AccessControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorController : CommonController<Visitor, VisitorDto, VisitorInsertDto, VisitorUpdateDto>
    {
        public VisitorController(ICommonService<VisitorDto, VisitorInsertDto, VisitorUpdateDto> service) 
            : base(service) { }
    }
}
