using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repsitories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandHandler : IRequestHandler<DeleteRestaurantCommand>
{
    private readonly ILogger<DeleteRestaurantCommandHandler> _logger;
    private readonly IRestaurantsRepository _restaurantsRepository;

    public DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> logger ,
                                          IRestaurantsRepository restaurantsRepository)
    {
        _logger = logger;
        _restaurantsRepository = restaurantsRepository;
    }

    public async Task Handle(DeleteRestaurantCommand request , CancellationToken cancellationToken)
    {
        _logger.LogInformation("Delete restaurant by Id: {RestaurantId}" , request.Id);
        var restaurants = await _restaurantsRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Restaurant) , request.Id.ToString());

        await _restaurantsRepository.DeleteRestaurant(restaurants);
    }
}
