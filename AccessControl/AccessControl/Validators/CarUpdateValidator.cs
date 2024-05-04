using AccessControl.DTOs;
using FluentValidation;

namespace AccessControl.Validators
{
    public class CarUpdateValidator : AbstractValidator<CarUpdateDto>
    {
        public CarUpdateValidator() { }
    }
}
