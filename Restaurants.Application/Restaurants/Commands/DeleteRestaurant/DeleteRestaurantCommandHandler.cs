using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repsitories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandHandler : IRequestHandler<DeleteRestaurantCommand>
{
    private readonly ILogger<DeleteRestaurantCommandHandler> _logger;
    private readonly IRestaurantsRepository _restaurantsRepository;
    private readonly IRestaurantAuthorizationService _restaurantAuthorizationService;

    public DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> logger ,
                                          IRestaurantsRepository restaurantsRepository ,
                                          IRestaurantAuthorizationService restaurantAuthorizationService)
    {
        _logger = logger;
        _restaurantsRepository = restaurantsRepository;
        _restaurantAuthorizationService = restaurantAuthorizationService;
    }

    public async Task Handle(DeleteRestaurantCommand request , CancellationToken cancellationToken)
    {
        _logger.LogInformation("Delete restaurant by Id: {RestaurantId}" , request.Id);
        var restaurants = await _restaurantsRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Restaurant) , request.Id.ToString());

        if (!_restaurantAuthorizationService.Authorize(restaurants, ResourceOperation.Delete))
            throw new ForbidException();

        await _restaurantsRepository.DeleteRestaurant(restaurants);
    }
}
