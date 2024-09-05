using System.ComponentModel;

namespace BaseReservation.Application.ResponseDTOs;

public record ResponseDetallePedidoProductoDto
{
    public long Id { get; set; }

    [DisplayName("Detalle")]
    public long IdDetallePedido { get; set; }

    [DisplayName("Producto")]
    public short IdProducto { get; set; }

    public decimal Cantidad { get; set; }

    public virtual ResponseDetallePedidoDto DetallePedido { get; set; } = null!;

    public virtual ResponseProductoDTO Producto { get; set; } = null!;
}