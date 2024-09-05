namespace BaseReservation.Application.RequestDTOs;

public record RequestFacturaDto: RequestBaseDto
{
    public long Id { get; set; }

    public short IdCliente { get; set; }

    public string NombreCliente { get; set; } = null!;

    public long? IdPedido { get; set; }

    public DateOnly Fecha { get; set; }

    public byte IdTipoPago { get; set; }

    public short Consecutivo { get; set; }

    public byte IdImpuesto { get; set; }

    public decimal PorcentajeImpuesto { get; set; }

    public decimal SubTotal { get; set; }

    public decimal MontoImpuesto { get; set; }

    public decimal MontoTotal { get; set; }

    public byte IdSucursal { get; set; }

    public IEnumerable<RequestDetalleFacturaDto>? DetalleFacturas { get; set; }
}