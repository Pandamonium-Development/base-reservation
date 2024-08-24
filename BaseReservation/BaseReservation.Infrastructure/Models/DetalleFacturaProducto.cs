using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Models;

[Table("DetalleFacturaProducto")]
[Index("IdDetalleFactura", Name = "IX_DetalleFacturaProducto_IdDetalleFactura")]
[Index("IdProducto", Name = "IX_DetalleFacturaProducto_IdProducto")]
public partial class DetalleFacturaProducto
{
    [Key]
    public long Id { get; set; }

    public long IdDetalleFactura { get; set; }

    public short IdProducto { get; set; }

    [Column(TypeName = "decimal(6, 2)")]
    public decimal Cantidad { get; set; }

    [ForeignKey("IdDetalleFactura")]
    [InverseProperty("DetalleFacturaProductos")]
    public virtual DetalleFactura IdDetalleFacturaNavigation { get; set; } = null!;

    [ForeignKey("IdProducto")]
    [InverseProperty("DetalleFacturaProductos")]
    public virtual Producto IdProductoNavigation { get; set; } = null!;
}
