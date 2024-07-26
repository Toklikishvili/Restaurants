using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Restaurants;

namespace Restaurants.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddAplication(this IServiceCollection services)
    {
        var aplicationAssembly = typeof(ServiceCollectionExtensions).Assembly;

        services.AddScoped<IRestaurantsService , RestaurantsService>();

        services.AddAutoMapper(aplicationAssembly);

        services.AddValidatorsFromAssembly(aplicationAssembly)
            .AddFluentValidationAutoValidation();
    }
}
