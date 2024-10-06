namespace BaseReservation.Application.RequestDTOs;

public record RequestBranchDto : RequestBaseDto
{
    public byte Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Telephone { get; set; }

    public string Email { get; set; } = null!;

    public short DistrictId { get; set; }

    public string? Address { get; set; }

    public bool Active { get; set; }
}