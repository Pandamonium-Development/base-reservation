namespace BaseReservation.Application.RequestDTOs;

public record RequestCantonDto
{
    public string Name { get; set; } = null!;

    public long ProvinceId { get; set; }
}