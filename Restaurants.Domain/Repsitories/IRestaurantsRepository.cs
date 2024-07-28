using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repsitories;

public interface IRestaurantsRepository
{
    Task<IEnumerable<Restaurant>> GetAllAsync();
    Task<Restaurant?> GetByIdAsync(int id);
    Task<int> Create(Restaurant entity);
    Task DeleteRestaurant(Restaurant entity);
    Task SaveChanges();
}
