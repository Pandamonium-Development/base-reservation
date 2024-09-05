using System.ComponentModel;

namespace BaseReservation.Application.ResponseDTOs;

public record ResponseDetalleFacturaProductoDto
{
    public long Id { get; set; }

    [DisplayName("Detalle")]
    public long IdDetalleFactura { get; set; }

    [DisplayName("Producto")]
    public short IdProducto { get; set; }

    public decimal Cantidad { get; set; }

    public virtual ResponseDetalleFacturaDto DetalleFactura { get; set; } = null!;

    public virtual ResponseProductoDto Producto { get; set; } = null!;
}