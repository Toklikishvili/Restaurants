using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Moq;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repsitories;
using Xunit;

namespace Restaurants.Infrastructure.Authorization.Requirements.Tests;

public class CreatedMultipleRestaurantsRequirementHandlerTests
{
    [Fact()]
    public async Task HandleRequirementAsync_UserHasCreatedMultipleRestaurants_ShouldSucced()
    {
        // arrange
        var currentUser = new CurrentUser("1" , "Test@test.com" , [] , null , null);
        var usercontextMock = new Mock<IUserContext>();
        usercontextMock.Setup(m => m.GetCurrentUser()).Returns(currentUser);

        var restaurants = new List<Restaurant>()
        {
            new()
            {
                OwnerId = currentUser.Id,
            },
            new()
            {
                OwnerId = currentUser.Id,
            },
            new()
            {
                OwnerId = "2",
            },
        };

        var restaurantRepositoryMock = new Mock<IRestaurantsRepository>();
        restaurantRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(restaurants);

        var requirement = new CreatedMultipleRestaurantsRequirement(2);
        var handler = new CreatedMultipleRestaurantsRequirementHandler(restaurantRepositoryMock.Object ,
            usercontextMock.Object);
        var context = new AuthorizationHandlerContext([requirement], null, null);

        // act

        await handler.HandleAsync(context);

        // assert

        context.HasSucceeded.Should().BeTrue();
    }

    [Fact()]
    public async Task HandleRequirementAsync_UserHasCreatedMultipleRestaurants_ShouldFail()
    {
        // arrange
        var currentUser = new CurrentUser("1" , "Test@test.com" , [] , null , null);
        var usercontextMock = new Mock<IUserContext>();
        usercontextMock.Setup(m => m.GetCurrentUser()).Returns(currentUser);

        var restaurants = new List<Restaurant>()
        {
            new()
            {
                OwnerId = currentUser.Id,
            },
            new()
            {
                OwnerId = "2",
            },
        };

        var restaurantRepositoryMock = new Mock<IRestaurantsRepository>();
        restaurantRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(restaurants);

        var requirement = new CreatedMultipleRestaurantsRequirement(2);
        var handler = new CreatedMultipleRestaurantsRequirementHandler(restaurantRepositoryMock.Object ,
            usercontextMock.Object);
        var context = new AuthorizationHandlerContext([requirement] , null , null);

        // act

        await handler.HandleAsync(context);

        // assert

        context.HasSucceeded.Should().BeFalse();
        context.HasFailed.Should().BeTrue();
    }
}