using FluentValidation;
using Para.Schema;

namespace Para.Business.Validation
{
    public class CustomerAddressRequestValidator : AbstractValidator<CustomerAddressRequest>
    {
        public CustomerAddressRequestValidator()
        {
            RuleFor(x => x.Country).NotEmpty().WithMessage("Country is required.");
            RuleFor(x => x.City).NotEmpty().WithMessage("City is required.");
            RuleFor(x => x.AddressLine).NotEmpty().WithMessage("Address line is required.");
            RuleFor(x => x.ZipCode).NotEmpty().WithMessage("Zip code is required.");
        }
    }
}
