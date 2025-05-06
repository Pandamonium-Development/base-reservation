using BaseReservation.Application.Dtos.Response.Enums;

namespace BaseReservation.Application.Dtos.Response.Authentication;

public record CurrentUser
{
    public long UserId { get; init; }

    public string? Email { get; init; }

    public RoleApplication? Role { get; init; }
}