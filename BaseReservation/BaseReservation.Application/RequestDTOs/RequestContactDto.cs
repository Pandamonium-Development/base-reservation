namespace BaseReservation.Application.RequestDTOs;

public record RequestContactDto : RequestBaseDto
{
    public short Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int Telephone { get; set; }

    public string Email { get; set; } = null!;

    public byte VendorId { get; set; }

    public bool Active { get; set; }
}