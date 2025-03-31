namespace BaseReservation.Application.RequestDTOs;

public record RequestUnitMeasureDto : RequestBaseDto
{
    public string Name { get; set; } = null!;

    public string Symbol { get; set; } = null!;
}