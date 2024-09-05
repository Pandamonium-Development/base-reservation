using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Models;

[Table("Factura")]
[Index("IdCliente", Name = "IX_Factura_IdCliente")]
[Index("IdImpuesto", Name = "IX_Factura_IdImpuesto")]
[Index("IdPedido", Name = "IX_Factura_IdPedido")]
[Index("IdSucursal", Name = "IX_Factura_IdSucursal")]
[Index("IdTipoPago", Name = "IX_Factura_IdTipoPago")]
public partial class Factura : BaseModel
{
    [Key]
    public long Id { get; set; }

    public byte IdSucursal { get; set; }

    public long? IdPedido { get; set; }

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

    [InverseProperty("IdFacturaNavigation")]
    public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; } = new List<DetalleFactura>();

    [ForeignKey("IdCliente")]
    [InverseProperty("Facturas")]
    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    [ForeignKey("IdImpuesto")]
    [InverseProperty("Facturas")]
    public virtual Impuesto IdImpuestoNavigation { get; set; } = null!;

    [ForeignKey("IdPedido")]
    [InverseProperty("Facturas")]
    public virtual Pedido? IdPedidoNavigation { get; set; }

    [ForeignKey("IdSucursal")]
    [InverseProperty("Facturas")]
    public virtual Sucursal IdSucursalNavigation { get; set; } = null!;

    [ForeignKey("IdTipoPago")]
    [InverseProperty("Facturas")]
    public virtual TipoPago IdTipoPagoNavigation { get; set; } = null!;
}