namespace BaseReservation.Application.RequestDTOs;

public record RequestCategoriaDto : RequestBaseDto
{
    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;
}