using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseReservation.Infrastructure.Enums;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Models;

[Table("Inventario")]
[Index("IdSucursal", Name = "IX_Inventario_IdSucursal")]
public partial class Inventario : BaseModel
{
    [Key]
    public short Id { get; set; }

    public byte IdSucursal { get; set; }

    public string Nombre { get; set; } = null!;

    public TipoInventario TipoInventario { get; set; }

    public bool Activo { get; set; }

    [ForeignKey("IdSucursal")]
    [InverseProperty("Inventarios")]
    public virtual Sucursal IdSucursalNavigation { get; set; } = null!;

    [InverseProperty("IdInventarioNavigation")]
    public virtual ICollection<InventarioProducto> InventarioProductos { get; set; } = new List<InventarioProducto>();
}