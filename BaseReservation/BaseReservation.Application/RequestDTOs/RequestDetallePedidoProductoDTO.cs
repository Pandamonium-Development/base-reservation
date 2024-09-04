namespace BaseReservation.Application.RequestDTOs;

public record RequestDetallePedidoProductoDto
{
    public long Id { get; set; }

    public long IdDetallePedido { get; set; }

    public short IdProducto { get; set; }

    public decimal Cantidad { get; set; }
}
