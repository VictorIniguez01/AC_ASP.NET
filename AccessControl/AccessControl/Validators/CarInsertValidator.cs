using AccessControl.DTOs;
using FluentValidation;

namespace AccessControl.Validators
{
    public class CarInsertValidator : AbstractValidator<CarInsertDto>
    {
        public CarInsertValidator()
        {
            RuleFor(x => x.CarBrand).NotEmpty();
            RuleFor(x => x.CarModel).NotEmpty();
            RuleFor(x => x.CarPlate).NotEmpty();
            RuleFor(x => x.CarColor).NotEmpty();
        }
    }
}
