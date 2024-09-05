namespace BaseReservation.Application.RequestDTOs;

public record RequestCategoriaDto: RequestBaseDto
{
    public byte Id { get; set; }

    public string Codigo { get; set; } = null!;

    public string Nombre { get; set; } = null!;
}
