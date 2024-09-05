namespace BaseReservation.Application.RequestDTOs;

public record RequestTipoServicioDto
{
    public byte Id { get; set; }

    public string Nombre { get; set; } = null!;

    public TimeOnly Duracion { get; set; }
}