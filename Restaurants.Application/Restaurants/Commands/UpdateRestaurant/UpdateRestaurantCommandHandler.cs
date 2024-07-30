using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repsitories;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandler : IRequestHandler<UpdateRestaurantCommand>
{
    private readonly ILogger<UpdateRestaurantCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IRestaurantsRepository _restaurantsRepository;

    public UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommandHandler> logger ,
                                          IMapper mapper , 
                                          IRestaurantsRepository restaurantsRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _restaurantsRepository = restaurantsRepository;
    }

    public async Task Handle(UpdateRestaurantCommand request , CancellationToken cancellationToken)
    {
        _logger.LogInformation("Update restaurant with Id: {RestaurantId} with {@UpdatedRestaurant}" , request.Id , request);
        var restaurants = await _restaurantsRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Restaurant) , request.Id.ToString());
        _mapper.Map(request , restaurants);

        await _restaurantsRepository.SaveChanges();
    }
}
