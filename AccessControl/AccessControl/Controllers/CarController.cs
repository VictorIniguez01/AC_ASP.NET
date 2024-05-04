using AccessControl.DTOs;
using AccessControl.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;

namespace AccessControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : CommonController<Car, CarDto, CarInsertDto, CarUpdateDto>
    {
        public CarController(ICommonService<CarDto, CarInsertDto, CarUpdateDto> carService)
            : base(carService) { }
    }
}
