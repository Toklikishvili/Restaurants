using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Repsitories;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryHandler : IRequestHandler<GetAllRestaurantsQuery , IEnumerable<RestaurantsDto>>
{
    private readonly ILogger<GetAllRestaurantsQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IRestaurantsRepository _restaurantsRepository;

    public GetAllRestaurantsQueryHandler(ILogger<GetAllRestaurantsQueryHandler> logger ,
                                         IMapper mapper , IRestaurantsRepository restaurantsRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _restaurantsRepository = restaurantsRepository;
    }

    public async Task<IEnumerable<RestaurantsDto>> Handle(GetAllRestaurantsQuery request , CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all restaurants");
        var restaurants = await _restaurantsRepository.GetAllMatchingAsync(request.SearchPhrase);

        var restaurantsDto = _mapper.Map<IEnumerable<RestaurantsDto>>(restaurants);
        return restaurantsDto!;
    }
}
