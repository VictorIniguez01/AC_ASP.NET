using AccessControl.DTOs;
using AccessControl.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
namespace AccessControl.Controllers
{
    public class CommonController<T, TDto, TIDto> : ControllerBase
    {
        protected ICommonService<TDto, TIDto> _service;
        protected IValidator<TIDto> _insertValidator;
        public CommonController(ICommonService<TDto, TIDto> service,
                                IValidator<TIDto> insertValidator)
        {
            _service = service;
            _insertValidator = insertValidator;
        }

        [HttpGet]
        public virtual async Task<IEnumerable<TDto>> Get()
            => await _service.Get();

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TDto>> GetById(int id)
        {
            TDto tDto = await _service.GetById(id);
            return tDto == null ? NotFound() : Ok(tDto);
        }

        [HttpPost]
        public virtual async Task<ActionResult<TDto>> Add(TIDto tiDto)
        {
            var validationResult = await _insertValidator.ValidateAsync(tiDto);
            if(!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            if(!_service.Validate(tiDto))
                return BadRequest(_service.Errors);

            TDto tDto = await _service.Add(tiDto);
            int tId = (tDto as ICommonDto)?.Id ?? 0;
            return CreatedAtAction(nameof(GetById), new { id = tId }, tDto);
        }

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult<TDto>> Delete(int id)
        {
            TDto tDto = await _service.Delete(id);
            return tDto == null ? NotFound() : Ok(tDto);
        }
    }
}
