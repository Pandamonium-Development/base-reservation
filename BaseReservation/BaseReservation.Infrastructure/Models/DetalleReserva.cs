using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Models;

[Table("DetalleReserva")]
[Index("IdReserva", Name = "IX_ReservaServicio_IdReserva")]
[Index("IdServicio", Name = "IX_ReservaServicio_IdServicio")]
public partial class DetalleReserva
{
    [Key]
    public int Id { get; set; }

    public int IdReserva { get; set; }

    public byte? IdServicio { get; set; }

    public short? IdProducto { get; set; }

    [ForeignKey("IdProducto")]
    [InverseProperty("DetalleReservas")]
    public virtual Producto? IdProductoNavigation { get; set; }

    [ForeignKey("IdReserva")]
    [InverseProperty("DetalleReservas")]
    public virtual Reserva IdReservaNavigation { get; set; } = null!;

    [ForeignKey("IdServicio")]
    [InverseProperty("DetalleReservas")]
    public virtual Servicio? IdServicioNavigation { get; set; }
}
