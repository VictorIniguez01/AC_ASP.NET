using AccessControl.DTOs;
using FluentValidation;

namespace AccessControl.Validators
{
    public class AccessVisitorInsertValidator : AbstractValidator<AccessVisitorInsertDto>
    {
        public AccessVisitorInsertValidator()
        {
            RuleFor(x => x.AccessVisitorEntry).NotEmpty();

            RuleFor(x => x.VisitorId).NotEmpty();
            RuleFor(x => x.VisitorId).GreaterThan(0);

            RuleFor(x => x.IsEntry).NotEmpty();
            RuleFor(x => x.IsGoingZone).NotEmpty();
        }
    }
}
