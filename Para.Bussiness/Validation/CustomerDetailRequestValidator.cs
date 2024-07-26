using FluentValidation;
using Para.Schema;

namespace Para.Business.Validation
{
    public class CustomerDetailRequestValidator : AbstractValidator<CustomerDetailRequest>
    {
        public CustomerDetailRequestValidator()
        {
            RuleFor(x => x.FatherName).NotEmpty().WithMessage("Father name is required.");
            RuleFor(x => x.MotherName).NotEmpty().WithMessage("Mother name is required.");
            RuleFor(x => x.EducationStatus).NotEmpty().WithMessage("Education status is required.");
            RuleFor(x => x.MontlyIncome).NotEmpty().WithMessage("Monthly income is required.");
            RuleFor(x => x.Occupation).NotEmpty().WithMessage("Occupation is required.");
        }
    }
}
