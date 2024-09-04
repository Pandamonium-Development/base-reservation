using BaseReservation.Application.ResponseDTOs.Enums;

namespace BaseReservation.Application.ResponseDTOs.Authentication;

public record CurrentUser
{
    public short IdUsuario { get; init; }

    public string? CorreoElectronico { get; init; }

    public Rol? Role { get; init; }
}