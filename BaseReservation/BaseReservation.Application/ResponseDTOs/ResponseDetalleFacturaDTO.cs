namespace BaseReservation.Application.ResponseDTOs;

public record ResponseDetalleFacturaDto
{
    public long Id { get; set; }

    public long IdFactura { get; set; }

    public byte IdServicio { get; set; }

    public byte NumeroLinea { get; set; }

    public short Cantidad { get; set; }

    public decimal TarifaServicio { get; set; }

    public decimal MontoSubtotal { get; set; }

    public decimal MontoImpuesto { get; set; }

    public decimal MontoTotal { get; set; }

    public virtual ICollection<ResponseDetalleFacturaProductoDto> DetalleFacturaProductos { get; set; } = new List<ResponseDetalleFacturaProductoDto>();

    public virtual ResponseFacturaDto Factura { get; set; } = null!;

    public virtual ResponseServicioDto? Servicio { get; set; } = null!;
}