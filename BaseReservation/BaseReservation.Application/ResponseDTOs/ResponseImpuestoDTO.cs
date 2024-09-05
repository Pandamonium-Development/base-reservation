namespace BaseReservation.Application.ResponseDTOs;

public record ResponseImpuestoDto
{
    public byte Id { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal Porcentaje { get; set; }

    public virtual ICollection<ResponseFacturaDto> Facturas { get; set; } = new List<ResponseFacturaDto>();
}