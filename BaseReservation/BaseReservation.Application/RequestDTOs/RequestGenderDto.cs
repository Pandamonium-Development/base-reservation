namespace BaseReservation.Application.RequestDTOs;

public record RequestGenderDto
{
    public byte Id { get; set; }

    public string Name { get; set; } = null!;
}