using BaseReservation.Application.ResponseDTOs.Authentication;
using BaseReservation.Application.ResponseDTOs.Enums;
using Microsoft.AspNetCore.Authorization;

namespace BaseReservation.WebAPI.Authorization;

/// <summary>
/// User identity hanlder class
/// </summary>
public class UserIdentityHandler : AuthorizationHandler<IdentifiedUser>
{
    /// <summary>
    /// Handle requirement for claims of user logged in
    /// </summary>
    /// <param name="context">Authorization context</param>
    /// <param name="requirement">identified User</param>
    /// <returns>Task</returns>
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IdentifiedUser requirement)
    {
        var httpContext = (HttpContext)context.Resource!;
        var claimFinder = new ClaimFinder(context.User.Claims);

        if (claimFinder.UserId != null && claimFinder.Role != null && claimFinder.Email != null)
        {
            httpContext.Items["CurrentUser"] = new CurrentUser
            {
                UserId = short.Parse(claimFinder.UserId!.Value),
                Email = claimFinder.Email!.Value,
                Role = (Role)Enum.Parse(typeof(Role), claimFinder.Role!.Value.ToUpper())
            };
        }

        context.Succeed(requirement);
        return Task.CompletedTask;
    }
}