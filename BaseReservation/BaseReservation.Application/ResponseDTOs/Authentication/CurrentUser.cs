using BaseReservation.Application.ResponseDTOs.Enums;

namespace BaseReservation.Application.ResponseDTOs.Authentication;

public record CurrentUser
{
    public short UserId { get; init; }

    public string? Email { get; init; }

    public Role? Role { get; init; }
}