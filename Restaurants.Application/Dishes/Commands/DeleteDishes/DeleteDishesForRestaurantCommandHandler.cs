using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repsitories;

namespace Restaurants.Application.Dishes.Commands.DeleteDishes;

public class DeleteDishesForRestaurantCommandHandler : IRequestHandler<DeleteDishesForRestaurantCommand>
{
    private readonly ILogger<DeleteDishesForRestaurantCommandHandler> _logger;
    private readonly IRestaurantsRepository _restaurantsRepository;
    private readonly IDishiesRepository _dishiesRepository;

    public DeleteDishesForRestaurantCommandHandler(ILogger<DeleteDishesForRestaurantCommandHandler> logger ,
                                                   IRestaurantsRepository restaurantsRepository ,
                                                   IDishiesRepository dishiesRepository)
    {
        _logger = logger;
        _restaurantsRepository = restaurantsRepository;
        _dishiesRepository = dishiesRepository;
    }

    public async Task Handle(DeleteDishesForRestaurantCommand request , CancellationToken cancellationToken)
    {
        _logger.LogWarning("Removing all dish from restaurant with Id: {RestaurantId}" , request.RestaurantId);

        var restaurant = await _restaurantsRepository.GetByIdAsync(request.RestaurantId)
            ?? throw new NotFoundException(nameof(Restaurant) , request.RestaurantId.ToString());

        await _dishiesRepository.Delete(restaurant.Dishes);
    }
}