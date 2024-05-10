using AccessControl.DTOs;
using AccessControl.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AccessControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorController : ControllerBase
    {
        ICreateService<VisitorDto, VisitorInsertDto> _createService;
        IReadService<VisitorDto> _readService;
        IDeleteService<VisitorDto> _deleteService;
        IValidator<VisitorInsertDto> _insertValidator;
        public VisitorController(ICreateService<VisitorDto, VisitorInsertDto> createService,
                                 IReadService<VisitorDto> readService,
                                 IDeleteService<VisitorDto> deleteService,
                                 IValidator<VisitorInsertDto> insertValidator)
        {
            _createService = createService;
            _readService = readService;
            _deleteService = deleteService;
            _insertValidator = insertValidator;
        }

        [HttpGet("all")]
        [Authorize(Roles = "UserResidential")]
        public virtual async Task<IEnumerable<VisitorDto>> Get()
            => await _readService.Get();


        [HttpGet("{id}")]
        [Authorize(Roles = "UserZone,UserResidential")]
        public virtual async Task<ActionResult<VisitorDto>> GetById(int id)
        {
            VisitorDto tDto = await _readService.GetById(id);
            return tDto == null ? NotFound() : Ok(tDto);
        }


        [HttpGet("user/{userAcId}")]
        [Authorize(Roles = "UserZone")]
        public async Task<IActionResult> GetByUserId(int userAcId)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var idClaim = identity.Claims.FirstOrDefault(x => x.Type == "id");

            if (idClaim == null)
                return NotFound();

            return Ok(await _readService.GetByUserId(Int32.Parse(idClaim.Value)));
        }


        [HttpPost("add")]
        [Authorize(Roles = "UserResidential")]
        public virtual async Task<ActionResult<VisitorDto>> Add(VisitorInsertDto tiDto)
        {
            var validationResult = await _insertValidator.ValidateAsync(tiDto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            if (!_createService.Validate(tiDto))
                return BadRequest(_createService.Errors);

            VisitorDto tDto = await _createService.Add(tiDto);
            return CreatedAtAction(nameof(GetById), new { id = tDto.VisitorId }, tDto);
        }


        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "UserResidential")]
        public virtual async Task<ActionResult<VisitorDto>> Delete(int id)
        {
            VisitorDto tDto = await _deleteService.Delete(id);
            return tDto == null ? NotFound() : Ok(tDto);
        }
    }
}
