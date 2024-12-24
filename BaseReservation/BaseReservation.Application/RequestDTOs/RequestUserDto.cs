namespace BaseReservation.Application.RequestDTOs;

public record RequestUserDto : RequestBaseDto
{
    public short Id { get; set; }

    public string CardId { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int Telephone { get; set; }

    public string Email { get; set; } = null!;

    public short DistrictId { get; set; }

    public string? Address { get; set; }

    public DateOnly Birthday { get; set; }

    public string Password { get; set; } = null!;

    public byte GenderId { get; set; }

    public bool Active { get; set; }

    public string? ProfilePictureUrl { get; set; }

    public byte RoleId { get; set; }
}