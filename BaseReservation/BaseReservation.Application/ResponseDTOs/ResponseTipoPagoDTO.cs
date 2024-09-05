namespace BaseReservation.Application.ResponseDTOs;

public record ResponseTipoPagoDto
{
    public byte Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public int Referencia { get; set; }

    public virtual ICollection<ResponseFacturaDTO> Facturas { get; set; } = new List<ResponseFacturaDTO>();
}