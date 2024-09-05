using BaseReservation.Application.ResponseDTOs.Enums;

namespace BaseReservation.Application.RequestDTOs;

public record RequestInventarioProductoMovimientoDto: RequestBaseDto
{
    public long Id { get; set; }

    public long IdInventarioProducto { get; set; }

    public TipoMovimientoInventario TipoMovimiento { get; set; }

    public decimal Cantidad { get; set; }
}