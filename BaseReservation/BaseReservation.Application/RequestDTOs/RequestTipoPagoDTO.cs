namespace BaseReservation.Application.RequestDTOs;

public record RequestTipoPagoDto
{
    public byte Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public int Referencia { get; set; }
}