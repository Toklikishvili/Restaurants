using FluentValidation;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Validators;

public class CreateRestaurantDtoValidator : AbstractValidator<CreateRestaurantDto>
{
    private readonly List<string> valideCategories = ["Italian" , "Mexican" , "Georgian"];
    public CreateRestaurantDtoValidator()
    {
        RuleFor(dto => dto.Name)
            .Length(3 , 100);
        RuleFor(dto => dto.Description)
            .NotEmpty().WithMessage("Description is required");

        RuleFor(dto => dto.Category)
            .Must(valideCategories.Contains)
            .WithMessage("Insert valid category");
        //RuleFor(dto => dto.Category)
        //    .NotEmpty().WithMessage("Insert valid category");

        RuleFor(dto => dto.ContactEmail)
            .EmailAddress()
            .WithMessage("Please provide a valid email address");

        RuleFor(dto => dto.PostalCode)
            .Matches(@"^\d{2}-\d{3}$")
            .WithMessage("Please provide a valid postal code (XX-XXX).");
    }
}
