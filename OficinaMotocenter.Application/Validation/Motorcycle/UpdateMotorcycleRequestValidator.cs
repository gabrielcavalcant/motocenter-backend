using FluentValidation;
using OficinaMotocenter.Application.Dto.Requests.Motorcycle;

namespace OficinaMotocenter.Application.Validation.Motorcycle
{
    /// <summary>
    /// Validator for updating a motorcycle request. Ensures that the update data is valid
    /// before being processed by the application.
    /// </summary>
    public class UpdateMotorcycleRequestValidator : AbstractValidator<UpdateMotorcycleRequest>
    {
        public UpdateMotorcycleRequestValidator()
        {
            // Name, if provided, must be between 1 and 100 characters.
            RuleFor(x => x.Name)
                .Length(1, 100).WithMessage("The Name must be less than 100 characters");

            // Plate, if provided, must be between 7 and 10 characters.
            RuleFor(x => x.Plate)
                .Length(7, 10).WithMessage("The plate must be less than 10 characters");

            // Year of manufacture must be between 1885 and the current year, if provided.
            RuleFor(x => x.YearManufacture)
                .InclusiveBetween(1885, DateTime.Now.Year).WithMessage("The year of manufacture must be between 1885 and the current year.");
        }
    }
}
