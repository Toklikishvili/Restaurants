using MediatR;

namespace Restaurants.Application.Users.Command.UpdateUserDeatails;

public class UpdateUserDeatailsCommand : IRequest
{
    public DateOnly? DateOfBirth { get; set; }
    public string? Nationality { get; set; }
}
