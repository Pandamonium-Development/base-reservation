using BaseReservation.Application.ResponseDTOs.Base;
using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Application.ResponseDTOs;

public record ResponseFacturaDTO : BaseEntity
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

    public virtual ICollection<ResponseDetalleFacturaDTO> DetalleFacturas { get; set; } = new List<ResponseDetalleFacturaDTO>();

    public virtual ResponseClienteDTO Cliente { get; set; } = null!;

    public virtual ResponseImpuestoDTO Impuesto { get; set; } = null!;

    public virtual ResponseTipoPagoDTO TipoPago { get; set; } = null!;

    public virtual ResponsePedidoDTO? Pedido { get; set; } = null!;

    public virtual Sucursal Sucursal { get; set; } = null!;
}