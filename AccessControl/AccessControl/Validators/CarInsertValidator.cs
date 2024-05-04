using AccessControl.DTOs;
using FluentValidation;

namespace AccessControl.Validators
{
    public class CarInsertValidator : AbstractValidator<CarInsertDto>
    {
        public CarInsertValidator() { }
    }
}
