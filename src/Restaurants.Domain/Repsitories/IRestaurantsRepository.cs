using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repsitories;

public interface IRestaurantsRepository
{
    Task<IEnumerable<Restaurant>> GetAllAsync();
    Task<Restaurant?> GetByIdAsync(int id);
    Task<int> Create(Restaurant entity);
    Task DeleteRestaurant(Restaurant entity);
    Task SaveChanges();
    Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(string? searchPhrase , int pageSize , int pageNumber , string? sortBy , SortDirection sortDirection);
}
