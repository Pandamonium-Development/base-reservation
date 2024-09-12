using System.Security.Claims;

namespace BaseReservation.WebAPI.Authorization;

/// <summary>
/// Claim finder class
/// </summary>
/// <typeparam name="Claim">List of claims passed on default constructor</typeparam>
public class ClaimFinder(IEnumerable<Claim> claims)
{
    /// <summary>
    /// User id property
    /// </summary>
    /// <returns></returns>
    public Claim? IdUsuario { get => claims.FirstOrDefault(m => m.Type == "IdUsuario"); }

    /// <summary>
    /// Email property
    /// </summary>
    /// <returns></returns>
    public Claim? CorreoElectronico { get => claims.FirstOrDefault(m => m.Type == "CorreoElectronico"); }

    /// <summary>
    /// Role property
    /// </summary>
    /// <returns></returns>
    public Claim? Role { get => claims.FirstOrDefault(m => m.Type == ClaimTypes.Role); }
}