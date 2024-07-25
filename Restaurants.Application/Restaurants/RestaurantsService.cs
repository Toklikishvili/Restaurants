using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repsitories;

namespace Restaurants.Application.Restaurants;

internal class RestaurantsService(IRestaurantsRepository restaurantsRepository ,
                                  ILogger<RestaurantsService> logger) : IRestaurantsService
{
    public async Task<IEnumerable<RestaurantsDto?>> GetAllRestaurants()
    {
        logger.LogInformation("Getting all restaurants");
        var restaurants = await restaurantsRepository.GetAllAsync();
        var restaurantsDto = restaurants.Select(RestaurantsDto.FromEntity);

        return restaurantsDto!;
    }

    public async Task<RestaurantsDto?> GetById(int id)
    {
        logger.LogInformation($"Getting restaurant {id}");
        var restaurants = await restaurantsRepository.GetByIdAsync(id);
        var restaurantDto = RestaurantsDto.FromEntity(restaurants);

        return restaurantDto;
    }
}
