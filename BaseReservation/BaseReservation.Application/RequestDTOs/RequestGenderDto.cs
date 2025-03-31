namespace BaseReservation.Application.RequestDTOs;

public record RequestGenderDto : RequestBaseDto
{
    public string Name { get; set; } = null!;
}