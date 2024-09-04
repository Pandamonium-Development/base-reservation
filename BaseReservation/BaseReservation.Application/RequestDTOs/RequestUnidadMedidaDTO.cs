namespace BaseReservation.Application.RequestDTOs;

public record RequestUnidadMedidaDto
{
    public byte Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Simbolo { get; set; } = null!;
}