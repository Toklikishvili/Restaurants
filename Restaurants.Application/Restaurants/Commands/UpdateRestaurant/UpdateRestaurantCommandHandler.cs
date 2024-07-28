using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Repsitories;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandler : IRequestHandler<UpdateRestaurantCommand , bool>
{
    private readonly ILogger<UpdateRestaurantCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IRestaurantsRepository _restaurantsRepository;

    public UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommandHandler> logger ,
                                          IMapper mapper , IRestaurantsRepository restaurantsRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _restaurantsRepository = restaurantsRepository;
    }

    public async Task<bool> Handle(UpdateRestaurantCommand request , CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Update restaurant with Id: {request.Id}");
        var restaurants = await _restaurantsRepository.GetByIdAsync(request.Id);
        if (restaurants == null)
            return false;

        _mapper.Map(request , restaurants);

        await _restaurantsRepository.SaveChanges();

        return true;
    }
}
