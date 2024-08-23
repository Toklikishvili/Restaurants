using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repsitories;

namespace Restaurants.Application.Dishes.Commands.CreateDish;

public class CreateDishCommandHandler : IRequestHandler<CreateDishCommand, int>
{
    private readonly ILogger<CreateDishCommandHandler> _logger;
    private readonly IRestaurantsRepository _restaurantsRepository;
    private readonly IDishiesRepository _dishiesRepository;
    private readonly IMapper _mapper;
    private readonly IRestaurantAuthorizationService _restaurantAuthorizationService;

    public CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger , IRestaurantsRepository restaurantsRepository , IDishiesRepository dishiesRepository , IMapper mapper , IRestaurantAuthorizationService restaurantAuthorizationService)
    {
        _logger = logger;
        _restaurantsRepository = restaurantsRepository;
        _dishiesRepository = dishiesRepository;
        _mapper = mapper;
        _restaurantAuthorizationService = restaurantAuthorizationService;
    }

    public async Task<int> Handle(CreateDishCommand request , CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating a new restaurant {@Restaurant}" , request);
        var restaurant = await _restaurantsRepository.GetByIdAsync(request.RestaurantId);
        if (restaurant == null) throw new NotFoundException(nameof(Restaurant) , request.RestaurantId.ToString());

        if (!_restaurantAuthorizationService.Authorize(restaurant , ResourceOperation.Create))
            throw new ForbidException();

        var dish = _mapper.Map<Dish>(request);

       return await _dishiesRepository.Create(dish);
    }
}
