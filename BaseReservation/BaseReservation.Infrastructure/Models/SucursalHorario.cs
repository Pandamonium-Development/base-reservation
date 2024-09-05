using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Models;

[Table("SucursalHorario")]
[Index("IdHorario", Name = "IX_SucursalHorario_IdHorario")]
[Index("IdSucursal", Name = "IX_SucursalHorario_IdSucursal")]
public partial class SucursalHorario
{
    [Key]
    public short Id { get; set; }

    public byte IdSucursal { get; set; }

    public short IdHorario { get; set; }

    [ForeignKey("IdHorario")]
    [InverseProperty("SucursalHorarios")]
    public virtual Horario IdHorarioNavigation { get; set; } = null!;

    [ForeignKey("IdSucursal")]
    [InverseProperty("SucursalHorarios")]
    public virtual Sucursal IdSucursalNavigation { get; set; } = null!;

    [InverseProperty("IdSucursalHorarioNavigation")]
    public virtual ICollection<SucursalHorarioBloqueo> SucursalHorarioBloqueos { get; set; } = new List<SucursalHorarioBloqueo>();
}