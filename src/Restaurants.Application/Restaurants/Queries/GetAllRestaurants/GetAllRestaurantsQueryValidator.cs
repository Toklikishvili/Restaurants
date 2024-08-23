using FluentValidation;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryValidator : AbstractValidator<GetAllRestaurantsQuery>
{
    private readonly int [] allowPageSize = [5 , 10 , 15 , 30];
    private readonly string [] allowedSortByColumnNames = [nameof(RestaurantsDto.Name),
    nameof(RestaurantsDto.Category),
    nameof(RestaurantsDto.Description)];

    public GetAllRestaurantsQueryValidator()
    {
        RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1);

        RuleFor(r => r.PageSize)
            .Must(value => allowPageSize.Contains(value))
            .WithMessage($"Page size must be in [{string.Join("," , allowPageSize)}]");

        RuleFor(r => r.SortBy)
            .Must(value => allowedSortByColumnNames.Contains(value))
            .When(q => q.SortBy != null)
            .WithMessage($"Sort by is optional, or must be in [{string.Join("," , allowedSortByColumnNames)}]");
    }
}
