namespace BaseReservation.Application.RequestDTOs;

public record RequestProvinciaDto
{
    public byte Id { get; set; }

    public string Nombre { get; set; } = null!;
}