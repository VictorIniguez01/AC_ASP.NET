using AccessControl.DTOs;
using AccessControl.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;

namespace AccessControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorController : CommonController<Visitor, VisitorDto, VisitorInsertDto>
    {
        public VisitorController(ICommonService<VisitorDto, VisitorInsertDto> service,
                                 IValidator<VisitorInsertDto> insertValidator) 
            : base(service, insertValidator) { }
    }
}
