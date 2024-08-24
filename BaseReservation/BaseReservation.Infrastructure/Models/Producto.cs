using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Models;

[Table("Producto")]
[Index("IdCategoria", Name = "IX_Producto_IdCategoria")]
[Index("IdUnidadMedida", Name = "IX_Producto_IdUnidadMedida")]
public partial class Producto
{
    [Key]
    public short Id { get; set; }

    [StringLength(70)]
    public string Nombre { get; set; } = null!;

    [StringLength(150)]
    public string Descripcion { get; set; } = null!;

    [StringLength(50)]
    public string Marca { get; set; } = null!;

    public byte IdCategoria { get; set; }

    [Column(TypeName = "money")]
    public decimal Costo { get; set; }

    [Column("SKU")]
    [StringLength(50)]
    public string Sku { get; set; } = null!;

    public byte IdUnidadMedida { get; set; }

    public bool Activo { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime FechaCreacion { get; set; }

    [StringLength(70)]
    public string UsuarioCreacion { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? FechaModificacion { get; set; }

    [StringLength(70)]
    public string? UsuarioModificacion { get; set; }

    [InverseProperty("IdProductoNavigation")]
    public virtual ICollection<DetalleFacturaProducto> DetalleFacturaProductos { get; set; } = new List<DetalleFacturaProducto>();

    [InverseProperty("IdProductoNavigation")]
    public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; } = new List<DetalleFactura>();

    [InverseProperty("IdProductoNavigation")]
    public virtual ICollection<DetallePedidoProducto> DetallePedidoProductos { get; set; } = new List<DetallePedidoProducto>();

    [InverseProperty("IdProductoNavigation")]
    public virtual ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();

    [InverseProperty("IdProductoNavigation")]
    public virtual ICollection<DetalleReserva> DetalleReservas { get; set; } = new List<DetalleReserva>();

    [ForeignKey("IdCategoria")]
    [InverseProperty("Productos")]
    public virtual Categorium IdCategoriaNavigation { get; set; } = null!;

    [ForeignKey("IdUnidadMedida")]
    [InverseProperty("Productos")]
    public virtual UnidadMedidum IdUnidadMedidaNavigation { get; set; } = null!;

    [InverseProperty("IdProductoNavigation")]
    public virtual ICollection<InventarioProducto> InventarioProductos { get; set; } = new List<InventarioProducto>();
}
