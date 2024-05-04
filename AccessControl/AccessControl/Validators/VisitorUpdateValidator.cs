using AccessControl.DTOs;
using FluentValidation;

namespace AccessControl.Validators
{
    public class VisitorUpdateValidator :  AbstractValidator<VisitorUpdateDto>
    {
        public VisitorUpdateValidator() { }
    }
}
