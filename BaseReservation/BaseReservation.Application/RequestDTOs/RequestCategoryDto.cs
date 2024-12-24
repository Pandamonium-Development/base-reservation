namespace BaseReservation.Application.RequestDTOs;

public record RequestCategoriaDto : RequestBaseDto
{
    public byte Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;
}