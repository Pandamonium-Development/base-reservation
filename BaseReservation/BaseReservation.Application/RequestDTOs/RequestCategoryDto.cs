namespace BaseReservation.Application.RequestDTOs;

public record RequestCategoryDto : RequestBaseDto
{
    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;
}