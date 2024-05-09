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

        [HttpGet]
        public virtual async Task<IEnumerable<CarDto>> Get()
            => await _readService.Get();

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<CarDto>> GetById(int id)
        {
            CarDto tDto = await _readService.GetById(id);
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
        public virtual async Task<ActionResult<CarDto>> Add(CarInsertDto tiDto)
        {
            var validationResult = await _insertValidator.ValidateAsync(tiDto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            if (!_createService.Validate(tiDto))
                return BadRequest(_createService.Errors);

            CarDto tDto = await _createService.Add(tiDto);
            int tId = (tDto as ICommonDto)?.Id ?? 0;
            return CreatedAtAction(nameof(GetById), new { id = tId }, tDto);
        }

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult<CarDto>> Delete(int id)
        {
            CarDto tDto = await _deleteService.Delete(id);
            return tDto == null ? NotFound() : Ok(tDto);
        }
    }
}
