using AutoMapper;
using FluentAssertions;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Domain.Entities;
using Xunit;

namespace Restaurants.Application.Restaurants.Dtos.Tests;

public class RestaurantsProfileTests
{
    private readonly IMapper _mapper;

    public RestaurantsProfileTests()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<RestaurantsProfile>();
        });

        _mapper = configuration.CreateMapper();
    }

    [Fact()]
    public void CreateMap_ForRestaurantToRestaurantDto_MapsCorrectly()
    {
        // arrange

        var restaurant = new Restaurant()
        {
            Id = 11 ,
            Name = "Owner2 Restaurant1" ,
            Description = "Italian Restaurant" ,
            Category = "Italian" ,
            HasDelivery = true ,
            ContactEmail = "spaghetti@gmai.com" ,
            ContactNumber = "345-1234" ,
            Address = new Address()
            {
                City = "Rome" ,
                Street = "Main Street" ,
                PostalCode = "10-010"
            }
        };

        // act

        var restaurantDto = _mapper.Map<RestaurantsDto>(restaurant);

        // assert

        restaurantDto.Should().NotBeNull();
        restaurantDto.Id.Should().Be(restaurant.Id);
        restaurantDto.Name.Should().Be(restaurant.Name);
        restaurantDto.Description.Should().Be(restaurant.Description);
        restaurantDto.Category.Should().Be(restaurant.Category);
        restaurantDto.HasDelivery.Should().Be(restaurant.HasDelivery);
        restaurantDto.City.Should().Be(restaurant.Address.City);
        restaurantDto.Street.Should().Be(restaurant.Address.Street);
        restaurantDto.PostalCode.Should().Be(restaurant.Address.PostalCode);
    }

    [Fact()]
    public void CreateMap_ForCreateRestaurantCommandToRestaurant_MapsCorrectly()
    {
        // arrange

        var command = new CreateRestaurantCommand()
        {
            Name = "Owner2 Restaurant1" ,
            Description = "Italian Restaurant" ,
            Category = "Italian" ,
            HasDelivery = true ,
            ContactEmail = "spaghetti@gmai.com" ,
            ContactNumber = "345-1234" ,
            City = "Rome" ,
            Street = "Main Street" ,
            PostalCode = "10-010"
        };

        // act

        var restaurant = _mapper.Map<Restaurant>(command);

        // assert

        restaurant.Should().NotBeNull();
        restaurant.Id.Should().Be(restaurant.Id);
        restaurant.Name.Should().Be(restaurant.Name);
        restaurant.Description.Should().Be(restaurant.Description);
        restaurant.Category.Should().Be(restaurant.Category);
        restaurant.HasDelivery.Should().Be(restaurant.HasDelivery);
        restaurant.Address.City.Should().Be(restaurant.Address.City);
        restaurant.Address.Street.Should().Be(restaurant.Address.Street);
        restaurant.Address.PostalCode.Should().Be(restaurant.Address.PostalCode);
    }

    [Fact()]
    public void CreateMap_ForUpdateRestaurantCommandToRestaurant_MapsCorrectly()
    {
        // arrange

        var command = new UpdateRestaurantCommand()
        {
            Id = 11 ,
            Name = "Owner2 Restaurant1" ,
            Description = "Italian Restaurant" ,
            HasDelivery = false
        };

        // act

        var restaurant = _mapper.Map<Restaurant>(command);

        // assert

        restaurant.Should().NotBeNull();
        restaurant.Id.Should().Be(command.Id);
        restaurant.Name.Should().Be(command.Name);
        restaurant.Description.Should().Be(command.Description);
        restaurant.HasDelivery.Should().Be(command.HasDelivery);
    }
}