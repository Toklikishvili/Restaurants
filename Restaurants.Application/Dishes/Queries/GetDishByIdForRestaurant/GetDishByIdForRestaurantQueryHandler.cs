using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Dto;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repsitories;

namespace Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant;

public class GetDishByIdForRestaurantQueryHandler : IRequestHandler<GetDishByIdForRestaurantQuery , DishDto>
{
    private readonly ILogger<GetDishByIdForRestaurantQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IRestaurantsRepository _restaurantsRepository;

    public GetDishByIdForRestaurantQueryHandler(ILogger<GetDishByIdForRestaurantQueryHandler> logger ,
                                                IMapper mapper ,
                                                IRestaurantsRepository restaurantsRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _restaurantsRepository = restaurantsRepository;
    }

    public async Task<DishDto> Handle(GetDishByIdForRestaurantQuery request , CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving dish:{DishId}, for restaurant with Id: {RestaurantId}" , request.DishId , request.RestaurantId);
        var restaurant = await _restaurantsRepository.GetByIdAsync(request.RestaurantId)
            ?? throw new NotFoundException(nameof(Restaurant) , request.RestaurantId.ToString());

        var dish = restaurant.Dishes.Find(d => d.Id == request.DishId);
        if (dish == null)
            throw new NotFoundException(nameof(Dish) , request.DishId.ToString());

        var result = _mapper.Map<DishDto>(dish);

        return result;
    }
}
