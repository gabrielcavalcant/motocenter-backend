using FluentValidation;
using OficinaMotocenter.Application.Dto.Requests.Motorcycle;

namespace OficinaMotocenter.Application.Validation.Motorcycle
{
    /// <summary>
    /// Validator for creating a motorcycle request. Ensures that the request data is valid
    /// before being processed by the application.
    /// </summary>
    public class CreateMotorcycleRequestValidator : AbstractValidator<CreateMotorcycleRequest>
    {
        public CreateMotorcycleRequestValidator()
        {
            // Name is required and must be between 1 and 100 characters.
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("The Name is required")
                .Length(1, 100).WithMessage("The Name must be less than 100 characters");

            // Plate is required and must be between 7 and 10 characters.
            RuleFor(x => x.Plate)
                .NotEmpty().WithMessage("The Plate is required")
                .Length(7, 10).WithMessage("The plate must be less than 10 characters");

            // Year of manufacture is required and must be between 1885 and the current year.
            RuleFor(x => x.YearManufacture)
                .NotEmpty().WithMessage("The Year Manufacture is required")
                .InclusiveBetween(1885, DateTime.Now.Year).WithMessage("The year of manufacture must be between 1885 and the current year.");

            // Type is required.
            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("The Type of motorcycle is required");
        }
    }
}
