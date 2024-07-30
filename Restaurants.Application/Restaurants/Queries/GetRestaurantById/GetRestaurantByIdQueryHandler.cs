using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repsitories;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById;

public class GetRestaurantByIdQueryhandler : IRequestHandler<GetRestaurantByIdQuery , RestaurantsDto>
{
    private readonly ILogger<GetRestaurantByIdQueryhandler> _logger;
    private readonly IMapper _mapper;
    private readonly IRestaurantsRepository _restaurantsRepository;

    public GetRestaurantByIdQueryhandler(ILogger<GetRestaurantByIdQueryhandler> logger ,
                                         IMapper mapper , 
                                         IRestaurantsRepository restaurantsRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _restaurantsRepository = restaurantsRepository;
    }

    public async Task<RestaurantsDto> Handle(GetRestaurantByIdQuery request , CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting restaurant by Id: {RestaurantId}" , request.Id);
        var restaurants = await _restaurantsRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Restaurant) , request.Id.ToString());

        var restaurantDto = _mapper.Map<RestaurantsDto>(restaurants);

        return restaurantDto;
    }
}
