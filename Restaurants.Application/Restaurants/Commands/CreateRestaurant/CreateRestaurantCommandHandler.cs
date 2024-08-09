using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repsitories;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandHandler : IRequestHandler<CreateRestaurantCommand , int>
{
    private readonly ILogger<CreateRestaurantCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IRestaurantsRepository _restaurantsRepository;
    private readonly IUserContext _userContext;

    public CreateRestaurantCommandHandler(ILogger<CreateRestaurantCommandHandler> logger ,
                                          IMapper mapper , IRestaurantsRepository restaurantsRepository , IUserContext userContext)
    {
        _logger = logger;
        _mapper = mapper;
        _restaurantsRepository = restaurantsRepository;
        _userContext = userContext;
    }

    public async Task<int> Handle(CreateRestaurantCommand request , CancellationToken cancellationToken)
    {
        var currentUser = _userContext.GetCurrentUser();

        _logger.LogInformation("{UserEmail} [{UserId}] is creating a new restaurant {@DishRequest}",
            currentUser.Email, currentUser.Id ,request);

        var restaurant = _mapper.Map<Restaurant>(request);
        restaurant.OwnerId = currentUser.Id;

        int id = await _restaurantsRepository.Create(restaurant);

        return id;
    }
}
