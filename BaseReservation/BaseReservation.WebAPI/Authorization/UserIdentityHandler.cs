using BaseReservation.Application.ResponseDTOs.Authentication;
using BaseReservation.Application.ResponseDTOs.Enums;
using Microsoft.AspNetCore.Authorization;

namespace BaseReservation.WebAPI.Authorization;

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

        if (claimFinder.IdUsuario != null && claimFinder.Role != null && claimFinder.CorreoElectronico != null)
        {
            httpContext.Items["CurrentUser"] = new CurrentUser
            {
                IdUsuario = short.Parse(claimFinder.IdUsuario!.Value),
                CorreoElectronico = claimFinder.CorreoElectronico!.Value,
                Role = (Rol)Enum.Parse(typeof(Rol), claimFinder.Role!.Value.ToUpper())
            };
        }

        context.Succeed(requirement);
        return Task.CompletedTask;
    }
}