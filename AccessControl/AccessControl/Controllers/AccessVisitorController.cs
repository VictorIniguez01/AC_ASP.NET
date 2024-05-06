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
    public class AccessVisitorController : CommonController<AccessVisitor, AccessVisitorDto, AccessVisitorInsertDto>
    {
        private IUpdateService<AccessVisitorDto, AccessVisitorUpdateDto> _updateService;
        private IValidator<AccessVisitorUpdateDto> _updateValidator;
        public AccessVisitorController(ICreateService<AccessVisitorDto, AccessVisitorInsertDto> createService,
                                       IReadService<AccessVisitorDto> readService,
                                       IDeleteService<AccessVisitorDto> deleteService,
                                       IValidator<AccessVisitorInsertDto> insertValidator,
                                       IValidator<AccessVisitorUpdateDto> updateValidator,
                                       IUpdateService<AccessVisitorDto, AccessVisitorUpdateDto> updateService)
            : base(createService, readService, deleteService, insertValidator)
        {
            _updateService = updateService;
            _updateValidator = updateValidator;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AccessVisitorDto>> Update(int id, AccessVisitorUpdateDto accessVisitorUpdateDto)
        {
            var validationResult = await _updateValidator.ValidateAsync(accessVisitorUpdateDto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            AccessVisitorDto accessVisitorDto = await _updateService.Update(id, accessVisitorUpdateDto);
            return accessVisitorDto == null ? NotFound() : Ok(accessVisitorDto);
        }
    }
}
