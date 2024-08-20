using FluentValidation.TestHelper;
using Xunit;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant.Tests;

public class CreateRestaurantCommandValidatorTests
{
    [Fact()]
    public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
    {
        // arrange

        var command = new CreateRestaurantCommand()
        {
            Name = "Owner" ,
            Category = "Italian" ,
            ContactEmail = "spaghetti@gmai.com" ,
            PostalCode = "34-123" ,
            Description = "Description" ,
        };

        var validator = new CreateRestaurantCommandValidator();

        // act

        var result = validator.TestValidate(command);

        // assert

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact()]
    public void Validator_ForInvalidCommand_ShouldNotHaveValidationErrors()
    {
        // arrange

        var command = new CreateRestaurantCommand()
        {
            Name = "Ow" ,
            Category = "Itali" ,
            ContactEmail = "@gmai.com" ,
            PostalCode = "34123" ,
            Description = "Descriptio" ,
        };

        var validator = new CreateRestaurantCommandValidator();

        // act

        var result = validator.TestValidate(command);

        // assert

        result.ShouldHaveValidationErrorFor(c => c.Name);
        result.ShouldHaveValidationErrorFor(c => c.Category);
        result.ShouldHaveValidationErrorFor(c => c.ContactEmail);
        result.ShouldHaveValidationErrorFor(c => c.PostalCode);
    }

    [Theory()]
    [InlineData("Italian")]
    [InlineData("Mexican")]
    [InlineData("Georgian")]
    public void Validator_ForValidCategory_ShouldNotHaveValidationErrorsForCategoryProperty(string category)
    {
        // arrange
        var validator = new CreateRestaurantCommandValidator();
        var command = new CreateRestaurantCommand { Category = category };

        //act

        var result = validator.TestValidate(command);

        //assert
        result.ShouldNotHaveValidationErrorFor(c => c.Category);
    }

    [Theory()]
    [InlineData("12099")]
    [InlineData("102-33")]
    [InlineData("12 345")]
    [InlineData("12-345 30")]
    public void Validator_ForInValidCategory_ShouldNotHaveValidationErrorsForPostalCodeProperty(string postalCode)
    {
        // arrange
        var validator = new CreateRestaurantCommandValidator();
        var command = new CreateRestaurantCommand { PostalCode = postalCode };

        //act

        var result = validator.TestValidate(command);

        //assert
        result.ShouldHaveValidationErrorFor(c => c.PostalCode);
    }
}