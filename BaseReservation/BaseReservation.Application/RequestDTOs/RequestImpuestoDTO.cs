namespace BaseReservation.Application.RequestDTOs;

public record RequestImpuestoDto
{
    public byte Id { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal Porcentaje { get; set; }
}
