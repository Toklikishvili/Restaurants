using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repsitories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;

internal class RestaurantsRepository(RestaurantsDbContext dbContext) : IRestaurantsRepository
{
    public async Task<int> Create(Restaurant entity)
    {
        dbContext.Restaurants.Add(entity);
        await dbContext.SaveChangesAsync();

        return entity.Id;
    }

    public async Task DeleteRestaurant(Restaurant entity)
    {
        dbContext.Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        var restaurants = await dbContext.Restaurants.ToListAsync();

        return restaurants;
    }

    public async Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(string? searchPhrase , int pageSize , int pageNumber)
    {

        var searchPhraseLower = searchPhrase?.ToLower();

        var baseQuery = dbContext
            .Restaurants
            .Where(r => searchPhraseLower == null
            || (r.Name.ToLower().Contains(searchPhraseLower)
            || r.Description.ToLower().Contains(searchPhraseLower)));

        var totalCount = await baseQuery.CountAsync();

        var restaurants = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        return (restaurants, totalCount);
    }

    public async Task<Restaurant?> GetByIdAsync(int id)
    {
        var restaurants = await dbContext.Restaurants
            .Include(r => r.Dishes)
            .FirstOrDefaultAsync(x => x.Id == id);

        return restaurants;
    }

    public async Task SaveChanges() => await dbContext.SaveChangesAsync();
}
