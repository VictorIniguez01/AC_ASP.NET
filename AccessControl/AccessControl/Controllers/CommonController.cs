using AccessControl.DTOs;
using AccessControl.Services;
using Microsoft.AspNetCore.Mvc;
namespace AccessControl.Controllers
{
    public class CommonController<T, TDto, TIDto, TUDto> : ControllerBase
    {
        protected ICommonService<TDto, TIDto, TUDto> _service;
        public CommonController(ICommonService<TDto, TIDto, TUDto> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<TDto>> Get()
            => await _service.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<TDto>> GetById(int id)
        {
            TDto tDto = await _service.GetById(id);
            return tDto == null ? NotFound() : Ok(tDto);
        }

        [HttpPost]
        public async Task<ActionResult<TDto>> Add(TIDto tiDto)
        {
            TDto tDto = await _service.Add(tiDto);
            int tId = (tDto as ICommonDto)?.Id ?? 0;
            return CreatedAtAction(nameof(GetById), new { id = tId }, tDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TDto>> Update(int id, TUDto tuDto)
        {
            TDto tDto = await _service.Update(id, tuDto);
            return tDto == null ? NotFound() : Ok(tDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TDto>> Delete(int id)
        {
            TDto tDto = await _service.Delete(id);
            return tDto == null ? NotFound() : Ok(tDto);
        }
    }
}
