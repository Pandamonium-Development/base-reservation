using BaseReservation.Application.ResponseDTOs.Base;
using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Application.ResponseDTOs;

public record ResponseFacturaDto : BaseEntity
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

    public virtual ICollection<ResponseDetalleFacturaDto> DetalleFacturas { get; set; } = new List<ResponseDetalleFacturaDto>();

    public virtual ResponseClienteDto Cliente { get; set; } = null!;

    public virtual ResponseImpuestoDto Impuesto { get; set; } = null!;

    public virtual ResponseTipoPagoDto TipoPago { get; set; } = null!;

    public virtual ResponsePedidoDto? Pedido { get; set; } = null!;

    public virtual Sucursal Sucursal { get; set; } = null!;
}