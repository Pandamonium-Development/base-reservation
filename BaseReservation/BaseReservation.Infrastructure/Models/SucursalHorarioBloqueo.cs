using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Models;

[Table("SucursalHorarioBloqueo")]
[Index("IdSucursalHorario", Name = "IX_SucursalHorarioBloqueo_IdSucursalHorario")]
public partial class SucursalHorarioBloqueo
{
    [Key]
    public long Id { get; set; }

    public short IdSucursalHorario { get; set; }

    public TimeOnly HoraInicio { get; set; }

    public TimeOnly HoraFin { get; set; }

    public bool Activo { get; set; }

    [ForeignKey("IdSucursalHorario")]
    [InverseProperty("SucursalHorarioBloqueos")]
    public virtual SucursalHorario IdSucursalHorarioNavigation { get; set; } = null!;
}