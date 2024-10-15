using FluentValidation;
using OficinaMotocenter.Application.Dto.Requests.Customer;

namespace OficinaMotocenter.Application.Validation.Customer
{
    public class CreateCustomerRequestValidator : AbstractValidator<CreateCustomerRequest>
    {
        public CreateCustomerRequestValidator()
        {
            // Name is required and must be between 1 and 100 characters.
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("The Name is required")
                .Length(1, 100).WithMessage("The Name must be less than 100 characters");

            // Plate is required and must be between 7 and 10 characters.
            RuleFor(x => x.Cpf)
                .NotEmpty().WithMessage("The Cpf is required")
                .Length(11).WithMessage("Cpf must be 11 characters long");

            // Cpf is required and must be between 1885 and the current year.
            RuleFor(x => x.Telephone)
                .NotEmpty().WithMessage("The Year Manufacture is required")
                .Length(12).WithMessage("Telephone must be 12 characters long");
        }
    }
}
