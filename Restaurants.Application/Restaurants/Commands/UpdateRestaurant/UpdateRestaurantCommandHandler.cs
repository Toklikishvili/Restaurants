using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repsitories;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandler : IRequestHandler<UpdateRestaurantCommand>
{
    private readonly ILogger<UpdateRestaurantCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IRestaurantsRepository _restaurantsRepository;
    private readonly IRestaurantAuthorizationService _restaurantAuthorizationService;

    public UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommandHandler> logger ,
                                          IMapper mapper ,
                                          IRestaurantsRepository restaurantsRepository ,
                                          IRestaurantAuthorizationService restaurantAuthorizationService)
    {
        _logger = logger;
        _mapper = mapper;
        _restaurantsRepository = restaurantsRepository;
        _restaurantAuthorizationService = restaurantAuthorizationService;
    }

    public async Task Handle(UpdateRestaurantCommand request , CancellationToken cancellationToken)
    {
        _logger.LogInformation("Update restaurant with Id: {RestaurantId} with {@UpdatedRestaurant}" , request.Id , request);
        var restaurants = await _restaurantsRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Restaurant) , request.Id.ToString());

        if (!_restaurantAuthorizationService.Authorize(restaurants , ResourceOperation.Update))
            throw new ForbidException();

        _mapper.Map(request , restaurants);

        await _restaurantsRepository.SaveChanges();
    }
}
