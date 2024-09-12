using BaseReservation.Application.ResponseDTOs.Enums;
using BaseReservation.Utils;
using Microsoft.AspNetCore.Authorization;

namespace BaseReservation.WebAPI.Configuration;

/// <summary>
/// Authorization attribute for controllers
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class BaseReservationAuthorizeAttribute : AuthorizeAttribute
{
    public BaseReservationAuthorizeAttribute() : base() { }

    public BaseReservationAuthorizeAttribute(params Rol[] roles)
    {
        var allowedRolesAsStrings = roles.Select(x => StringExtension.Capitalize(Enum.GetName(typeof(Rol), x)!));
        Roles = string.Join(",", allowedRolesAsStrings);
    }
}