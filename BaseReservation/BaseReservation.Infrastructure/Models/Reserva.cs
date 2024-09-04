using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Models;

[Table("Reserva")]
[Index("IdCliente", Name = "IX_Reserva_IdCliente")]
[Index("IdSucursal", Name = "IX_Reserva_IdSucursal")]
public partial class Reserva
{
    [Key]
    public int Id { get; set; }

    public byte IdSucursal { get; set; }

    public short IdCliente { get; set; }

    [StringLength(80)]
    public string NombreCliente { get; set; } = null!;

    public DateOnly Fecha { get; set; }

    public TimeOnly Hora { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string Estado { get; set; } = null!;

    public bool Activo { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime FechaCreacion { get; set; }

    [StringLength(70)]
    public string UsuarioCreacion { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? FechaModificacion { get; set; }

    [StringLength(70)]
    public string? UsuarioModificacion { get; set; }

    [InverseProperty("IdReservaNavigation")]
    public virtual ICollection<DetalleReserva> DetalleReservas { get; set; } = new List<DetalleReserva>();

    [ForeignKey("IdCliente")]
    [InverseProperty("Reservas")]
    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    [ForeignKey("IdSucursal")]
    [InverseProperty("Reservas")]
    public virtual Sucursal IdSucursalNavigation { get; set; } = null!;

    [InverseProperty("IdReservaNavigation")]
    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    [InverseProperty("IdReservaNavigation")]
    public virtual ICollection<ReservaPregunta> ReservaPregunta { get; set; } = new List<ReservaPregunta>();
}
