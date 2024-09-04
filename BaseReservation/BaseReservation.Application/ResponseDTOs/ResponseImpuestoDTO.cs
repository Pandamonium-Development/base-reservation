namespace BaseReservation.Application.ResponseDTOs;

public record ResponseImpuestoDTO
{
    public byte Id { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal Porcentaje { get; set; }

    public virtual ICollection<ResponseFacturaDTO> Facturas { get; set; } = new List<ResponseFacturaDTO>();
}