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
        public VisitorController(ICreateService<VisitorDto, VisitorInsertDto> createService,
                                 IReadService<VisitorDto> readService,
                                 IDeleteService<VisitorDto> deleteService,
                                 IValidator<VisitorInsertDto> insertValidator) 
            : base(createService, readService, deleteService, insertValidator) { }
    }
}
