using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Models;

[Table("DetalleFactura")]
[Index("IdFactura", Name = "IX_DetalleFactura_IdFactura")]
[Index("IdServicio", Name = "IX_DetalleFactura_IdServicio")]
public partial class DetalleFactura
{
    [Key]
    public long Id { get; set; }

    public long IdFactura { get; set; }

    public byte? IdServicio { get; set; }

    public short? IdProducto { get; set; }

    public byte NumeroLinea { get; set; }

    public short Cantidad { get; set; }

    [Column(TypeName = "money")]
    public decimal TarifaServicio { get; set; }

    [Column(TypeName = "money")]
    public decimal MontoSubtotal { get; set; }

    [Column(TypeName = "money")]
    public decimal MontoImpuesto { get; set; }

    [Column(TypeName = "money")]
    public decimal MontoTotal { get; set; }

    [InverseProperty("IdDetalleFacturaNavigation")]
    public virtual ICollection<DetalleFacturaProducto> DetalleFacturaProductos { get; set; } = new List<DetalleFacturaProducto>();

    [ForeignKey("IdFactura")]
    [InverseProperty("DetalleFacturas")]
    public virtual Factura IdFacturaNavigation { get; set; } = null!;

    [ForeignKey("IdProducto")]
    [InverseProperty("DetalleFacturas")]
    public virtual Producto? IdProductoNavigation { get; set; }

    [ForeignKey("IdServicio")]
    [InverseProperty("DetalleFacturas")]
    public virtual Servicio? IdServicioNavigation { get; set; }
}