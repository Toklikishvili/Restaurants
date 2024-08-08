using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Users.Command.DeleteUserRole;

public class DeleteUserRoleCommandHandler : IRequestHandler<DeleteUserRoleCommand>
{
    private readonly ILogger<DeleteUserRoleCommandHandler> _logger;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public DeleteUserRoleCommandHandler(ILogger<DeleteUserRoleCommandHandler> logger ,
                                        UserManager<User> userManager ,
                                        RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task Handle(DeleteUserRoleCommand request , CancellationToken cancellationToken)
    {
        _logger.LogInformation("Delete user role: {@Request}" , request);

        var user = await _userManager.FindByEmailAsync(request.UserEmail)
           ?? throw new NotFoundException(nameof(User) , request.UserEmail);

        var role = await _roleManager.FindByNameAsync(request.RoleName)
            ?? throw new NotFoundException(nameof(IdentityRole) , request.RoleName);

        await _userManager.RemoveFromRoleAsync(user , role.Name!);
    }
}
