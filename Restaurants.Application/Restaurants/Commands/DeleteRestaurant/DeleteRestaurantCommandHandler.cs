using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Repsitories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandHandler : IRequestHandler<DeleteRestaurantCommand , bool>
{
    private readonly ILogger<DeleteRestaurantCommandHandler> _logger;
    private readonly IRestaurantsRepository _restaurantsRepository;

    public DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> logger ,
                                          IRestaurantsRepository restaurantsRepository)
    {
        _logger = logger;
        _restaurantsRepository = restaurantsRepository;
    }

    public async Task<bool> Handle(DeleteRestaurantCommand request , CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Delete restaurant by Id: {request.Id}");
        var restaurants = await _restaurantsRepository.GetByIdAsync(request.Id);
        if (restaurants == null)
            return false;

        await _restaurantsRepository.DeleteRestaurant(restaurants);
        return true;
    }
}
