namespace BaseReservation.Application.RequestDTOs;

public record RequestProvinceDto : RequestBaseDto
{
    public string Name { get; set; } = null!;
}