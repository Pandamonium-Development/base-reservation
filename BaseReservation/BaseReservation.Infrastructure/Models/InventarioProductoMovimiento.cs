using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseReservation.Infrastructure.Enums;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Models;

[Table("InventarioProductoMovimiento")]
[Index("IdInventarioProducto", Name = "IX_InventarioProductoMovimiento_IdInventarioProducto")]
public partial class InventarioProductoMovimiento : BaseModel
{
    [Key]
    public long Id { get; set; }

    public long IdInventarioProducto { get; set; }

    public TipoMovimientoInventario TipoMovimiento { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Cantidad { get; set; }

    [ForeignKey("IdInventarioProducto")]
    [InverseProperty("InventarioProductoMovimientos")]
    public virtual InventarioProducto IdInventarioProductoNavigation { get; set; } = null!;
}