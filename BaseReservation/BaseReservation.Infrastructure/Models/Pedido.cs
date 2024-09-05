using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Models;

[Table("Pedido")]
[Index("IdCliente", Name = "IX_Pedido_IdCliente")]
[Index("IdImpuesto", Name = "IX_Pedido_IdImpuesto")]
[Index("IdReserva", Name = "IX_Pedido_IdReserva")]
[Index("IdSucursal", Name = "IX_Pedido_IdSucursal")]
[Index("IdTipoPago", Name = "IX_Pedido_IdTipoPago")]
public partial class Pedido : BaseModel
{
    [Key]
    public long Id { get; set; }

    public byte IdSucursal { get; set; }

    public int IdReserva { get; set; }

    public short IdCliente { get; set; }

    [StringLength(160)]
    public string NombreCliente { get; set; } = null!;

    public DateOnly Fecha { get; set; }

    public byte IdTipoPago { get; set; }

    public short Consecutivo { get; set; }

    public byte IdImpuesto { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal PorcentajeImpuesto { get; set; }

    [Column(TypeName = "money")]
    public decimal SubTotal { get; set; }

    [Column(TypeName = "money")]
    public decimal MontoImpuesto { get; set; }

    [Column(TypeName = "money")]
    public decimal MontoTotal { get; set; }

    public byte IdEstadoPedido { get; set; }

    [InverseProperty("IdPedidoNavigation")]
    public virtual ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();

    [InverseProperty("IdPedidoNavigation")]
    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    [ForeignKey("IdCliente")]
    [InverseProperty("Pedidos")]
    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    [ForeignKey("IdEstadoPedido")]
    [InverseProperty("Pedidos")]
    public virtual EstadoPedido IdEstadoPedidoNavigation { get; set; } = null!;

    [ForeignKey("IdImpuesto")]
    [InverseProperty("Pedidos")]
    public virtual Impuesto IdImpuestoNavigation { get; set; } = null!;

    [ForeignKey("IdReserva")]
    [InverseProperty("Pedidos")]
    public virtual Reserva IdReservaNavigation { get; set; } = null!;

    [ForeignKey("IdSucursal")]
    [InverseProperty("Pedidos")]
    public virtual Sucursal IdSucursalNavigation { get; set; } = null!;

    [ForeignKey("IdTipoPago")]
    [InverseProperty("Pedidos")]
    public virtual TipoPago IdTipoPagoNavigation { get; set; } = null!;
}