using BaseReservation.Application.ResponseDTOs.Base;

namespace BaseReservation.Application.ResponseDTOs;

public record ResponsePedidoDTO : BaseEntity
{
    public long Id { get; set; }

    public short IdCliente { get; set; }

    public string NombreCliente { get; set; } = null!;

    public DateOnly Fecha { get; set; }

    public byte IdTipoPago { get; set; }

    public short Consecutivo { get; set; }

    public byte IdImpuesto { get; set; }

    public int IdReserva { get; set; }

    public decimal PorcentajeImpuesto { get; set; }

    public decimal SubTotal { get; set; }

    public decimal MontoImpuesto { get; set; }

    public decimal MontoTotal { get; set; }

    public char Estado { get; set; }

    public byte IdSucursal { get; set; }

    public virtual ICollection<ResponseDetallePedidoDTO> DetallePedidos { get; set; } = new List<ResponseDetallePedidoDTO>();

    public virtual ResponseClienteDTO Cliente { get; set; } = null!;

    public virtual ResponseImpuestoDTO Impuesto { get; set; } = null!;

    public virtual ResponseTipoPagoDto TipoPago { get; set; } = null!;

    public virtual ResponseReservaDto Reserva { get; set; } = null!;

    public virtual ResponseSucursalDto Sucursal { get; set; } = null!;
}