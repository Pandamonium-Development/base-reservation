using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Models;

[Table("InventarioProducto")]
[Index("IdInventario", Name = "IX_InventarioProducto_IdInventario")]
[Index("IdProducto", Name = "IX_InventarioProducto_IdProducto")]
public partial class InventarioProducto
{
    [Key]
    public long Id { get; set; }

    public short IdInventario { get; set; }

    public short IdProducto { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Disponible { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Minima { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Maxima { get; set; }

    public DateTime FechaCreacion { get; set; }

    public string UsuarioCreacion { get; set; } = null!;

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    [ForeignKey("IdInventario")]
    [InverseProperty("InventarioProductos")]
    public virtual Inventario IdInventarioNavigation { get; set; } = null!;

    [ForeignKey("IdProducto")]
    [InverseProperty("InventarioProductos")]
    public virtual Producto IdProductoNavigation { get; set; } = null!;

    [InverseProperty("IdInventarioProductoNavigation")]
    public virtual ICollection<InventarioProductoMovimiento> InventarioProductoMovimientos { get; set; } = new List<InventarioProductoMovimiento>();
}
