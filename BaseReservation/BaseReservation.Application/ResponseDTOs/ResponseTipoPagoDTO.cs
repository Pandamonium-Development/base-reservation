namespace BaseReservation.Application.ResponseDTOs;

public record ResponseTipoPagoDTO
{
    public byte Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public int Referencia { get; set; }

    public virtual ICollection<ResponseFacturaDto> Facturas { get; set; } = new List<ResponseFacturaDto>();
}