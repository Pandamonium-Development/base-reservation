namespace BaseReservation.Application.RequestDTOs;

public record RequestProvinceDto
{
    public byte Id { get; set; }

    public string Name { get; set; } = null!;
}