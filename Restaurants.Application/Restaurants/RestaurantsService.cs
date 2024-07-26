using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repsitories;

namespace Restaurants.Application.Restaurants;

internal class RestaurantsService(IRestaurantsRepository restaurantsRepository ,
                                  ILogger<RestaurantsService> logger,
                                  IMapper mapper) : IRestaurantsService
{
    public async Task<int> Create(CreateRestaurantDto dto)
    {
        logger.LogInformation("Creating a new restaurant");
        var restaurant = mapper.Map<Restaurant>(dto);
        int id = await restaurantsRepository.Create(restaurant);

        return id;
    }

    public async Task<IEnumerable<RestaurantsDto?>> GetAllRestaurants()
    {
        logger.LogInformation("Getting all restaurants");
        var restaurants = await restaurantsRepository.GetAllAsync();
        var restaurantsDto = mapper.Map<IEnumerable<RestaurantsDto>>(restaurants);

        return restaurantsDto!;
    }

    public async Task<RestaurantsDto?> GetById(int id)
    {
        logger.LogInformation($"Getting restaurant by Id: {id}");
        var restaurants = await restaurantsRepository.GetByIdAsync(id);
        var restaurantDto = mapper.Map<RestaurantsDto?>(restaurants);

        return restaurantDto;
    }
}
