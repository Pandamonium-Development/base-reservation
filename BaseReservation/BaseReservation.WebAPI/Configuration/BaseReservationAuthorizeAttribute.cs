using BaseReservation.Application.ResponseDTOs.Enums;
using BaseReservation.Utils;
using Microsoft.AspNetCore.Authorization;

namespace BaseReservation.WebAPI.Configuration;

/// <summary>
/// Authorization attribute for controllers
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class BaseReservationAuthorizeAttribute : AuthorizeAttribute
{
    /// <summary>
    /// Default constructor
    /// </summary>
    /// <returns></returns>
    public BaseReservationAuthorizeAttribute() : base() { }

    /// <summary>
    /// Overload constructor to pass list of roles
    /// </summary>
    /// <param name="roles">List of roles</param>
    public BaseReservationAuthorizeAttribute(params Rol[] roles)
    {
        var allowedRolesAsStrings = roles.Select(x => StringExtension.Capitalize(Enum.GetName(typeof(Rol), x)!));
        Roles = string.Join(",", allowedRolesAsStrings);
    }
}