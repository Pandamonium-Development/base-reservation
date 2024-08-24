using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Models;

[Table("DetallePedidoProducto")]
[Index("IdDetallePedido", Name = "IX_DetallePedidoProducto_IdDetallePedido")]
[Index("IdProducto", Name = "IX_DetallePedidoProducto_IdProducto")]
public partial class DetallePedidoProducto
{
    [Key]
    public long Id { get; set; }

    public long IdDetallePedido { get; set; }

    public short IdProducto { get; set; }

    [Column(TypeName = "decimal(6, 2)")]
    public decimal Cantidad { get; set; }

    [ForeignKey("IdDetallePedido")]
    [InverseProperty("DetallePedidoProductos")]
    public virtual DetallePedido IdDetallePedidoNavigation { get; set; } = null!;

    [ForeignKey("IdProducto")]
    [InverseProperty("DetallePedidoProductos")]
    public virtual Producto IdProductoNavigation { get; set; } = null!;
}
