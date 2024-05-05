using AccessControl.DTOs;
using FluentValidation;

namespace AccessControl.Validators
{
    public class AccessVisitorUpdateValidator : AbstractValidator<AccessVisitorUpdateDto>
    {
        public AccessVisitorUpdateValidator()
        {
            RuleFor(x => x.IsEntry).NotEmpty();
            RuleFor(x => x.IsGoingZone).NotEmpty();
        }
    }
}
