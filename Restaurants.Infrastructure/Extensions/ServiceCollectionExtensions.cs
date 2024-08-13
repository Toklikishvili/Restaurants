﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repsitories;
using Restaurants.Infrastructure.Authorization;
using Restaurants.Infrastructure.Authorization.Requirements;
using Restaurants.Infrastructure.Authorization.Services;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Repositories;
using Restaurants.Infrastructure.Seeders;

namespace Restaurants.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services , IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("RestaurantsDb");
        services.AddDbContext<RestaurantsDbContext>(options =>
        options.UseSqlServer(connectionString).EnableSensitiveDataLogging());

        services.AddIdentityApiEndpoints<User>()
            .AddRoles<IdentityRole>()
            .AddClaimsPrincipalFactory<RestaurantsUserClaimsPrincipalFactory>()
            .AddEntityFrameworkStores<RestaurantsDbContext>();

        services.AddScoped<IRestaurantSeeder , RestaurantSeeder>();
        services.AddScoped<IRestaurantsRepository , RestaurantsRepository>();
        services.AddScoped<IDishiesRepository , DishiesRepository>();
        services.AddAuthorizationBuilder()
            .AddPolicy(PolicyNames.HasNationality ,
            builder => builder.RequireClaim(AppClaimTypes.Nationality , "German" , "Polish"))
            .AddPolicy(PolicyNames.Age ,
            builder => builder.AddRequirements(new MinimumAgeRequirment(20)))
            .AddPolicy(PolicyNames.CreatedAtleast2Restaurants ,
            builder => builder.AddRequirements(new CreatedMultipleRestaurantsRequirement(2)));

        services.AddScoped<IAuthorizationHandler , MinimumAgeRequirmentHandler>();
        services.AddScoped<IAuthorizationHandler , CreatedMultipleRestaurantsRequirementHandler>();
        services.AddScoped<IRestaurantAuthorizationService , RestaurantAuthorizationService>();
    }
}
