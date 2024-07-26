using FluentValidation;
using Para.Schema;

namespace Para.Business.Validation
{
    public class CustomerPhoneRequestValidator : AbstractValidator<CustomerPhoneRequest>
    {
        public CustomerPhoneRequestValidator()
        {
            RuleFor(x => x.CountyCode).NotEmpty().WithMessage("Country code is required.");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone number is required.")
                                 .Matches(@"^\d+$").WithMessage("Phone number must be numeric.");
        }
    }
}
