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
    public class AccessVisitorController : ControllerBase
    {
        private ICreateService<AccessVisitorDto, AccessVisitorInsertDto> _createService;
        private IReadService<AccessVisitorDto> _readService;
        private IUpdateService<AccessVisitorDto, AccessVisitorUpdateDto> _updateService;
        private IDeleteService<AccessVisitorDto> _deleteService;
        private IValidator<AccessVisitorInsertDto> _insertValidator;
        private IValidator<AccessVisitorUpdateDto> _updateValidator;
        public AccessVisitorController(ICreateService<AccessVisitorDto, AccessVisitorInsertDto> createService,
                                       IReadService<AccessVisitorDto> readService,
                                       IDeleteService<AccessVisitorDto> deleteService,
                                       IValidator<AccessVisitorInsertDto> insertValidator,
                                       IValidator<AccessVisitorUpdateDto> updateValidator,
                                       IUpdateService<AccessVisitorDto, AccessVisitorUpdateDto> updateService)
        {
            _createService = createService;
            _readService = readService;
            _updateService = updateService;
            _deleteService = deleteService;
            _insertValidator = insertValidator;
            _updateValidator = updateValidator;
        }

        [HttpGet("all")]
        [Authorize(Roles = "UserResidential")]
        public async Task<IEnumerable<AccessVisitorDto>> Get()
            => await _readService.Get();


        [HttpGet("{id}")]
        [Authorize(Roles = "UserZone,UserResidential")]
        public async Task<IActionResult> GetById(int id)
        {
            AccessVisitorDto tDto = await _readService.GetById(id);
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
        public async Task<ActionResult<AccessVisitorDto>> Add(AccessVisitorInsertDto tiDto)
        {
            var validationResult = await _insertValidator.ValidateAsync(tiDto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            if (!_createService.Validate(tiDto))
                return BadRequest(_createService.Errors);

            AccessVisitorDto tDto = await _createService.Add(tiDto);
            return CreatedAtAction(nameof(GetById), new { id = tDto.AccessVisitorId }, tDto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "UserZone")]
        public async Task<ActionResult<AccessVisitorDto>> Update(int id, AccessVisitorUpdateDto accessVisitorUpdateDto)
        {
            var validationResult = await _updateValidator.ValidateAsync(accessVisitorUpdateDto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            AccessVisitorDto accessVisitorDto = await _updateService.Update(id, accessVisitorUpdateDto);
            return accessVisitorDto == null ? NotFound() : Ok(accessVisitorDto);
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "UserResidential")]
        public async Task<ActionResult<AccessVisitorDto>> Delete(int id)
        {
            AccessVisitorDto tDto = await _deleteService.Delete(id);
            return tDto == null ? NotFound() : Ok(tDto);
        }
    }
}
