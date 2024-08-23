using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Users.Command.UpdateUserDeatails;

public class UpdateUserDeatailsCommandHandler : IRequestHandler<UpdateUserDeatailsCommand>
{
    private readonly ILogger<UpdateUserDeatailsCommandHandler> _logger;
    private readonly IUserContext _userContext;
    private readonly IUserStore<User> _userStore;

    public UpdateUserDeatailsCommandHandler(ILogger<UpdateUserDeatailsCommandHandler> logger,
                                            IUserContext userContext,
                                            IUserStore<User> userStore)
    {
        _logger = logger;
        _userContext = userContext;
        _userStore = userStore;
    }

    public async Task Handle(UpdateUserDeatailsCommand request, CancellationToken cancellationToken)
    {
        var user = _userContext.GetCurrentUser();

        _logger.LogInformation("Updating user: {UserId}, with {@Request}", user!.Id, request);

        var dbUser = await _userStore.FindByIdAsync(user!.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(User), user!.Id);

        dbUser.Nationality = request.Nationality;
        dbUser.DateOfBirth = request.DateOfBirth;

        await _userStore.UpdateAsync(dbUser, cancellationToken);
    }
}
