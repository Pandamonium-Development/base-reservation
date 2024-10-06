namespace BaseReservation.Application.RequestDTOs;

public record RequestDistrictDto
{
    public short Id { get; set; }

    public string Name { get; set; } = null!;

    public byte CantonId { get; set; }
}