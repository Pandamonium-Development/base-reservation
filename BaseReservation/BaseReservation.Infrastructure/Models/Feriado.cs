using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseReservation.Infrastructure.Enums;

namespace BaseReservation.Infrastructure.Models;

[Table("Feriado")]
public partial class Feriado : BaseModel
{
    [Key]
    public byte Id { get; set; }

    [StringLength(80)]
    public string Nombre { get; set; } = null!;

    [StringLength(25)]
    public Mes Mes { get; set; }

    public byte Dia { get; set; }

    public bool Activo { get; set; }

    [InverseProperty("IdFeriadoNavigation")]
    public virtual ICollection<SucursalFeriado> SucursalFeriados { get; set; } = new List<SucursalFeriado>();
}