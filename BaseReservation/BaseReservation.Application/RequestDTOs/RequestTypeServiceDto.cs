namespace BaseReservation.Application.RequestDTOs;

public record RequestTypeServiceDto
{
    public byte Id { get; set; }

    public string Name { get; set; } = null!;

    public TimeOnly BaseDuration { get; set; }
}