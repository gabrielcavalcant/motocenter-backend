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

            // Cpf is required and must be 11 characters.
            RuleFor(x => x.Cpf)
                .NotEmpty().WithMessage("The Cpf is required")
                .Length(11, 11).WithMessage("The Cpf must be 11 characters long");

            // Telephone is required and must be 11 characters.
            RuleFor(x => x.Telephone)
                .NotEmpty().WithMessage("The Telephone is required")
                .Length(11,11).WithMessage("Telephone must be 11 characters long");
        }
    }
}
