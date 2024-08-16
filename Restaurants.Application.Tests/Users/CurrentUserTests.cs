using FluentAssertions;
using Restaurants.Domain.Constants;
using Xunit;

namespace Restaurants.Application.Users.Tests;

public class CurrentUserTests
{
    //TestMethod_Scenario_ExpectResult
    [Theory()]
    [InlineData(UserRoles.Admin)]
    [InlineData(UserRoles.User)]
    public void IsInRole_WithMachingRole_ShouldReturnTrue(string roleName)
    {
        //arrange
        var carrentUser = new CurrentUser("2" , "Toklika@gmail.com" , [UserRoles.Admin , UserRoles.User] , null , null);

        //act
        var isInRole = carrentUser.IsInRole(roleName);

        //assert

        isInRole.Should().BeTrue();
    }

    [Fact()]
    public void IsInRole_WithNoMachingRole_ShouldReturnFalse()
    {
        //arrange
        var carrentUser = new CurrentUser("2" , "Toklika@gmail.com" , [UserRoles.Admin , UserRoles.User] , null , null);

        //act
        var isInRole = carrentUser.IsInRole(UserRoles.Owner);

        //assert

        isInRole.Should().BeFalse();
    }

    [Fact()]
    public void IsInRole_WithNoMachingRoleCase_ShouldReturnFalse()
    {
        //arrange
        var carrentUser = new CurrentUser("2" , "Toklika@gmail.com" , [UserRoles.Admin , UserRoles.User] , null , null);

        //act
        var isInRole = carrentUser.IsInRole(UserRoles.Admin.ToLower());

        //assert

        isInRole.Should().BeFalse();
    }
}