namespace BaseReservation.Application.RequestDTOs;

public record RequestRoleDto : RequestBaseDto
{
    public byte Id { get; set; }

    public string Description { get; set; } = null!;

    public string Type { get; set; } = null!;

    public bool Active { get; set; }
}