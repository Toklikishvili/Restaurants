using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.Dto;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repsitories;

namespace Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;

public class GetDishesForRestaurantQueryHandler : IRequestHandler<GetDishesForRestaurantQuery , IEnumerable<DishDto>>
{
    private readonly ILogger<GetDishesForRestaurantQueryHandler> _logger;
    private readonly IRestaurantsRepository _restaurantsRepository;
    private readonly IDishiesRepository _dishiesRepository;
    private readonly IMapper _mapper;

    public GetDishesForRestaurantQueryHandler(ILogger<GetDishesForRestaurantQueryHandler> logger , 
                                              IRestaurantsRepository restaurantsRepository , 
                                              IDishiesRepository dishiesRepository , 
                                              IMapper mapper)
    {
        _logger = logger;
        _restaurantsRepository = restaurantsRepository;
        _dishiesRepository = dishiesRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DishDto>> Handle(GetDishesForRestaurantQuery request , CancellationToken cancellationToken)
    {
        _logger.LogInformation("Get dishes from restaurant with id: {RestaurantId}" , request.RestaurantId);
        var restaurant = await _restaurantsRepository.GetByIdAsync(request.RestaurantId);

        if (restaurant == null)
            throw new NotFoundException(nameof(Restaurant) , request.RestaurantId.ToString());

        var results = _mapper.Map<IEnumerable<DishDto>>(restaurant.Dishes);

        return results;
    }
}
