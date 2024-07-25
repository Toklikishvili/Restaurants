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
        }
    }

    private IEnumerable<Restaurant> GetRestaurants()
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
                Name = "McDonald",
                Category = "FastFood",
                Description = "McDonald is junk food like KFC",
                ContactEmail = "contactEmali@mcdonald.com",
                HasDelivery = true,
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
