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
    public class CarController : CommonController<Car, CarDto, CarInsertDto>
    {
        public CarController(ICreateService<CarDto, CarInsertDto> createService,
                             IReadService<CarDto> readService,
                             IDeleteService<CarDto> deleteService,
                             IValidator<CarInsertDto> insertValidator)
            : base(createService, readService, deleteService, insertValidator) { }
    }
}
