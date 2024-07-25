using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants;

public interface IRestaurantsService
{
    Task<IEnumerable<RestaurantsDto?>> GetAllRestaurants();
    Task<RestaurantsDto?> GetById(int id);
}