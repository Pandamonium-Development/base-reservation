namespace BaseReservation.Application.ResponseDTOs;

public record ResponseDetallePedidoDto
{
    public long Id { get; set; }

    public long IdPedido { get; set; }

    public byte IdServicio { get; set; }

    public byte NumeroLinea { get; set; }

    public short Cantidad { get; set; }

    public decimal TarifaServicio { get; set; }

    public decimal MontoSubtotal { get; set; }

    public decimal MontoImpuesto { get; set; }

    public decimal MontoTotal { get; set; }

    public virtual ICollection<ResponseDetallePedidoProductoDto> DetallePedidoProductos { get; set; } = new List<ResponseDetallePedidoProductoDto>();

    public virtual ResponsePedidoDto Pedido { get; set; } = null!;

    public virtual ResponseServicioDTO Servicio { get; set; } = null!;
}