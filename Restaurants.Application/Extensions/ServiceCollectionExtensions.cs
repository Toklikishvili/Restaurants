using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Restaurants.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddAplication(this IServiceCollection services)
    {
        var aplicationAssembly = typeof(ServiceCollectionExtensions).Assembly;

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(aplicationAssembly));
        services.AddAutoMapper(aplicationAssembly);
        services.AddValidatorsFromAssembly(aplicationAssembly)
            .AddFluentValidationAutoValidation();
    }
}
