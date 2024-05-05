using AccessControl.DTOs;
using FluentValidation;

namespace AccessControl.Validators
{
    public class VisitorInsertValidator : AbstractValidator<VisitorInsertDto>
    {
        public VisitorInsertValidator()
        {
            RuleFor(x => x.VisitorName).NotEmpty();
            RuleFor(x => x.VisitorLastName).NotEmpty();

            RuleFor(x => x.CarId).NotEmpty();
            RuleFor(x => x.CarId).GreaterThan(0);

            RuleFor(x => x.HouseId).NotEmpty();
            RuleFor(x => x.HouseId).GreaterThan(0);
        }
    }
}
