using AccessControl.DTOs;
using AccessControl.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;
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

        [HttpGet]
        public virtual async Task<IEnumerable<VisitorDto>> Get()
            => await _readService.Get();

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<VisitorDto>> GetById(int id)
        {
            VisitorDto tDto = await _readService.GetById(id);
            return tDto == null ? NotFound() : Ok(tDto);
        }

        [HttpPost]
        public async Task<IActionResult> GetByUserId(int userAcId)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var idClaim = identity.Claims.FirstOrDefault(x => x.Type == "id");

            if (idClaim == null)
                return NotFound();

            return Ok(await _readService.GetByUserId(Int32.Parse(idClaim.Value)));
        }

        [HttpPost("add")]
        public virtual async Task<ActionResult<VisitorDto>> Add(VisitorInsertDto tiDto)
        {
            var validationResult = await _insertValidator.ValidateAsync(tiDto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            if (!_createService.Validate(tiDto))
                return BadRequest(_createService.Errors);

            VisitorDto tDto = await _createService.Add(tiDto);
            int tId = (tDto as ICommonDto)?.Id ?? 0;
            return CreatedAtAction(nameof(GetById), new { id = tId }, tDto);
        }

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult<VisitorDto>> Delete(int id)
        {
            VisitorDto tDto = await _deleteService.Delete(id);
            return tDto == null ? NotFound() : Ok(tDto);
        }
    }
}
