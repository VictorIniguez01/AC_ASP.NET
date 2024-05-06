using AccessControl.DTOs;
using AccessControl.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
namespace AccessControl.Controllers
{
    public class CommonController<T, TDto, TIDto> : ControllerBase
    {
        protected ICreateService<TDto, TIDto> _createService;
        protected IReadService<TDto> _readService;
        protected IDeleteService<TDto> _deleteService;
        protected IValidator<TIDto> _insertValidator;
        public CommonController(ICreateService<TDto, TIDto> createService,
                                IReadService<TDto> readService,
                                IDeleteService<TDto> deleteService,
                                IValidator<TIDto> insertValidator)
        {
            _createService = createService;
            _readService = readService;
            _deleteService = deleteService;
            _insertValidator = insertValidator;
        }

        [HttpGet]
        public virtual async Task<IEnumerable<TDto>> Get()
            => await _readService.Get();

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TDto>> GetById(int id)
        {
            TDto tDto = await _readService.GetById(id);
            return tDto == null ? NotFound() : Ok(tDto);
        }

        [HttpPost]
        public virtual async Task<ActionResult<TDto>> Add(TIDto tiDto)
        {
            var validationResult = await _insertValidator.ValidateAsync(tiDto);
            if(!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            if(!_createService.Validate(tiDto))
                return BadRequest(_createService.Errors);

            TDto tDto = await _createService.Add(tiDto);
            int tId = (tDto as ICommonDto)?.Id ?? 0;
            return CreatedAtAction(nameof(GetById), new { id = tId }, tDto);
        }

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult<TDto>> Delete(int id)
        {
            TDto tDto = await _deleteService.Delete(id);
            return tDto == null ? NotFound() : Ok(tDto);
        }
    }
}
