using Microsoft.AspNetCore.Identity;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Seeders;

internal class RestaurantSeeder(RestaurantsDbContext dbContext) : IRestaurantSeeder
{
    public async Task Seed()
    {
        if (await dbContext.Database.CanConnectAsync())
        {
            if (!dbContext.Restaurants.Any())
            {

                var restaurants = GetRestaurants();
                dbContext.AddRange(restaurants);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Roles.Any())
            {
                var roles = GetRoles();
                dbContext.Roles.AddRange(roles);
                await dbContext.SaveChangesAsync();
            }
        }
    }

    private IEnumerable<IdentityRole> GetRoles()
    {
        List<IdentityRole> roles =
            [
               new (UserRoles.User)
               {
                   NormalizedName = UserRoles.User.ToUpper()
               },
               new (UserRoles.Owner)
               {
                   NormalizedName = UserRoles.Owner.ToUpper()
               },
               new (UserRoles.Admin)
               {
                   NormalizedName = UserRoles.Admin.ToUpper()
               },
            ];

        return roles;
    }

    private static IEnumerable<Restaurant> GetRestaurants()
    {
        List<Restaurant> restaurants =
            [
            new ()
            {
                Name = "KFC",
                Category = "Fast Food",
                Description = "KFC is an American fast food",
                ContactEmail = "contactEmail@kfc.com",
                HasDelivery = true,
                Dishes =
                [
                    new()
                    {
                        Name = "Hot Wings",
                        Description = "Hot Wings with spices(10pcs)",
                        Price = 7.90M,
                    },

                    new()
                    {
                        Name = "Hot Nuggets",
                        Description = "Chickent Nuggets(5Pcs)",
                        Price = 5.20M,
                    },
                 ],

                Address = new()
                {
                    City = "Tbilisi",
                    Street = "Rustaveli Avenue",
                    PostalCode = "0108",
                }
            },

            new ()
            {
                Name = "McDonald's",
                Category = "FastFood",
                Description = "McDonald is junk food like KFC",
                ContactEmail = "contactEmali@mcdonald.com",
                HasDelivery = true,
                Dishes =
                [
                    new()
                    {
                        Name = "Chesse Burger",
                        Description = "Burger with chesse",
                        Price = 15.50M,
                    },

                    new()
                    {
                        Name = "Fries",
                        Description = "Fried potatoes in oil",
                        Price = 7.8M,
                    },
                 ],

                Address = new()
                {
                    City = "Tbilisi",
                    Street = "Rustaveli Avenue",
                    PostalCode = "0105"
                }
            }
            ];

        return restaurants;
    }
}
