using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Restaurants.Domain.Constants;
using System.Security.Claims;
using Xunit;

namespace Restaurants.Application.Users.Tests;

public class UserContextTests
{
    [Fact()]
    public void GetCurrentUser_WithAuthenticatedUser_ShouldReturnCurrentUser()
    {
        //arrange
        var dateOfBirh = new DateOnly(1988 , 12 , 22);

        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        var claims = new List<Claim>()
        {
            new(ClaimTypes.NameIdentifier, "2"),
            new(ClaimTypes.Email, "Toklika@gmail.com"),
            new(ClaimTypes.Role, UserRoles.Admin),
            new(ClaimTypes.Role, UserRoles.User),
            new("Nationality", "German"),
            new("DateOfBirth", dateOfBirh.ToString("yyyy-MM-dd"))
        };

        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));

        httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext()
        {
            User = user
        });

        var userContext = new UserContext(httpContextAccessorMock.Object);

        //act
        var currentUser = userContext.GetCurrentUser();

        //assert
        currentUser.Should().NotBeNull();
        currentUser.Id.Should().Be("2");
        currentUser.Email.Should().Be("Toklika@gmail.com");
        currentUser.Roles.Should().ContainInOrder(UserRoles.Admin, UserRoles.User);
        currentUser.Nationality.Should().Be("German");
        currentUser.DateOfBirth.Should().Be(dateOfBirh);
    }

    [Fact()]
    public void GetCurrentUser_WithUserContextNotPresent_ThrowaInvalidOperationException()
    {
        //arrange
        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        httpContextAccessorMock.Setup(x => x.HttpContext).Returns((HttpContext)null);

        var userContext = new UserContext(httpContextAccessorMock.Object);

        //act
        Action action = () => userContext.GetCurrentUser();

        //assert
        action.Should()
            .Throw<InvalidOperationException>()
            .WithMessage("User context is not present");
    }
}