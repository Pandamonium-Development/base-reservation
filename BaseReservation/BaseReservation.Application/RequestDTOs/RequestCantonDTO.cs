namespace BaseReservation.Application.RequestDTOs;

public record RequestCantonDto
{
    public byte Id { get; set; }

    public string Name { get; set; } = null!;

    public byte ProvinceId { get; set; }
}