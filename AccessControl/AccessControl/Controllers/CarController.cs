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
    public class CarController : ControllerBase
    {
        private ICreateService<CarDto, CarInsertDto> _createService;
        private IReadService<CarDto> _readService;
        private IDeleteService<CarDto> _deleteService;
        private IValidator<CarInsertDto> _insertValidator;
        public CarController(ICreateService<CarDto, CarInsertDto> createService,
                             IReadService<CarDto> readService,
                             IDeleteService<CarDto> deleteService,
                             IValidator<CarInsertDto> insertValidator)
        {
            _createService = createService;
            _readService = readService;
            _deleteService = deleteService;
            _insertValidator = insertValidator;
        }

        [HttpGet("all")]
        [Authorize(Roles = "UserResidential")]
        public virtual async Task<IEnumerable<CarDto>> Get()
            => await _readService.Get();


        [HttpGet("{id}")]
        [Authorize(Roles = "UserZone,UserResidential")]
        public virtual async Task<ActionResult<CarDto>> GetById(int id)
        {
            CarDto tDto = await _readService.GetById(id);
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
        public virtual async Task<ActionResult<CarDto>> Add(CarInsertDto tiDto)
        {
            var validationResult = await _insertValidator.ValidateAsync(tiDto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            if (!_createService.Validate(tiDto))
                return BadRequest(_createService.Errors);

            CarDto tDto = await _createService.Add(tiDto);
            return CreatedAtAction(nameof(GetById), new { id = tDto.CarId }, tDto);
        }


        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "UserResidential")]
        public virtual async Task<ActionResult<CarDto>> Delete(int id)
        {
            CarDto tDto = await _deleteService.Delete(id);
            return tDto == null ? NotFound() : Ok(tDto);
        }
    }
}
