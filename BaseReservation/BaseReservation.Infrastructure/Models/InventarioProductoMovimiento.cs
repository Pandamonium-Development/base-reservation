using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseReservation.Infrastructure.Enums;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Models;

[Table("InventarioProductoMovimiento")]
[Index("IdInventarioProducto", Name = "IX_InventarioProductoMovimiento_IdInventarioProducto")]
public partial class InventarioProductoMovimiento
{
    [Key]
    public long Id { get; set; }

    public long IdInventarioProducto { get; set; }

    public TipoMovimientoInventario TipoMovimiento { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Cantidad { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime FechaCreacion { get; set; }

    [StringLength(70)]
    public string UsuarioCreacion { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? FechaModificacion { get; set; }

    [StringLength(70)]
    public string? UsuarioModificacion { get; set; }

    [ForeignKey("IdInventarioProducto")]
    [InverseProperty("InventarioProductoMovimientos")]
    public virtual InventarioProducto IdInventarioProductoNavigation { get; set; } = null!;
}
