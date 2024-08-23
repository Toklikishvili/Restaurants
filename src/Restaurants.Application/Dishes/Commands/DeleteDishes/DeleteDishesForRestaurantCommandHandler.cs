using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repsitories;

namespace Restaurants.Application.Dishes.Commands.DeleteDishes;

public class DeleteDishesForRestaurantCommandHandler : IRequestHandler<DeleteDishesForRestaurantCommand>
{
    private readonly ILogger<DeleteDishesForRestaurantCommandHandler> _logger;
    private readonly IRestaurantsRepository _restaurantsRepository;
    private readonly IDishiesRepository _dishiesRepository;
    private readonly IRestaurantAuthorizationService _restaurantAuthorizationService;

    public DeleteDishesForRestaurantCommandHandler(ILogger<DeleteDishesForRestaurantCommandHandler> logger ,
                                                   IRestaurantsRepository restaurantsRepository ,
                                                   IDishiesRepository dishiesRepository ,
                                                   IRestaurantAuthorizationService restaurantAuthorizationService)
    {
        _logger = logger;
        _restaurantsRepository = restaurantsRepository;
        _dishiesRepository = dishiesRepository;
        _restaurantAuthorizationService = restaurantAuthorizationService;
    }

    public async Task Handle(DeleteDishesForRestaurantCommand request , CancellationToken cancellationToken)
    {
        _logger.LogWarning("Removing all dish from restaurant with Id: {RestaurantId}" , request.RestaurantId);

        var restaurant = await _restaurantsRepository.GetByIdAsync(request.RestaurantId)
            ?? throw new NotFoundException(nameof(Restaurant) , request.RestaurantId.ToString());

        if (!_restaurantAuthorizationService.Authorize(restaurant , ResourceOperation.Delete))
            throw new ForbidException();

        await _dishiesRepository.Delete(restaurant.Dishes);
    }
}