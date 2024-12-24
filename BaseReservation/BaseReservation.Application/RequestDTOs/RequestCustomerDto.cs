namespace BaseReservation.Application.RequestDTOs;

public record RequestCustomerDto : RequestBaseDto
{
    public short Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int Telephone { get; set; }

    public short DistrictId { get; set; }

    public string? Address { get; set; }

    public bool Active { get; set; }
}