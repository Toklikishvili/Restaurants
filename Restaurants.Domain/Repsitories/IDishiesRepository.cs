using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repsitories;

public interface IDishiesRepository
{
    Task<int> Create(Dish entity);
}
