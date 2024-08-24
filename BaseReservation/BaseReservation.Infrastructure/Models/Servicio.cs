using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Models;

[Table("Servicio")]
[Index("IdTipoServicio", Name = "IX_Servicio_IdTipoServicio")]
public partial class Servicio
{
    [Key]
    public byte Id { get; set; }

    [StringLength(50)]
    public string Nombre { get; set; } = null!;

    [StringLength(150)]
    public string Descripcion { get; set; } = null!;

    public byte IdTipoServicio { get; set; }

    [Column(TypeName = "money")]
    public decimal Tarifa { get; set; }

    [StringLength(250)]
    public string? Observacion { get; set; }

    public bool Activo { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime FechaCreacion { get; set; }

    [StringLength(70)]
    public string UsuarioCreacion { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? FechaModificacion { get; set; }

    [StringLength(70)]
    public string? UsuarioModificacion { get; set; }

    [InverseProperty("IdServicioNavigation")]
    public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; } = new List<DetalleFactura>();

    [InverseProperty("IdServicioNavigation")]
    public virtual ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();

    [InverseProperty("IdServicioNavigation")]
    public virtual ICollection<DetalleReserva> DetalleReservas { get; set; } = new List<DetalleReserva>();

    [ForeignKey("IdTipoServicio")]
    [InverseProperty("Servicios")]
    public virtual TipoServicio IdTipoServicioNavigation { get; set; } = null!;
}
