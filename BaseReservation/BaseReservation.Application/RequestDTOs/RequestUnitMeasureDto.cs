namespace BaseReservation.Application.RequestDTOs;

public record RequestUnitMeasureDto
{
    public byte Id { get; set; }

    public string Name { get; set; } = null!;

    public string Symbol { get; set; } = null!;
}