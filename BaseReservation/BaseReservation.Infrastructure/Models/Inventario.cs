using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Models;

[Table("Inventario")]
[Index("IdSucursal", Name = "IX_Inventario_IdSucursal")]
public partial class Inventario
{
    [Key]
    public short Id { get; set; }

    public byte IdSucursal { get; set; }

    public string Nombre { get; set; } = null!;

    public string TipoInventario { get; set; } = null!;

    public bool Activo { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime FechaCreacion { get; set; }

    [StringLength(70)]
    public string UsuarioCreacion { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? FechaModificacion { get; set; }

    [StringLength(70)]
    public string? UsuarioModificacion { get; set; }

    [ForeignKey("IdSucursal")]
    [InverseProperty("Inventarios")]
    public virtual Sucursal IdSucursalNavigation { get; set; } = null!;

    [InverseProperty("IdInventarioNavigation")]
    public virtual ICollection<InventarioProducto> InventarioProductos { get; set; } = new List<InventarioProducto>();
}
