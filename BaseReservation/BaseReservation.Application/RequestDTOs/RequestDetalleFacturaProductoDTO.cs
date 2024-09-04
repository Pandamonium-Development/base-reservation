namespace BaseReservation.Application.RequestDTOs;

public record RequestDetalleFacturaProductoDto
{
    public long Id { get; set; }

    public long IdDetalleFactura { get; set; }

    public short IdProducto { get; set; }

    public decimal Cantidad { get; set; }
}
