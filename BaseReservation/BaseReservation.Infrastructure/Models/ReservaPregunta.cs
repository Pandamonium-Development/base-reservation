using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Models;

[Table("ReservaPregunta")]
[Index("IdReserva", Name = "IX_ReservaPregunta_IdReserva")]
public partial class ReservaPregunta : BaseModel
{
    [Key]
    public int Id { get; set; }

    public int IdReserva { get; set; }

    [StringLength(250)]
    public string Pregunta { get; set; } = null!;

    [StringLength(250)]
    public string? Respuesta { get; set; }

    public bool Activo { get; set; }

    [ForeignKey("IdReserva")]
    [InverseProperty("ReservaPregunta")]
    public virtual Reserva IdReservaNavigation { get; set; } = null!;
}