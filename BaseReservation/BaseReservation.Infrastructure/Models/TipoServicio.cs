using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseReservation.Infrastructure.Models;

[Table("TipoServicio")]
public partial class TipoServicio
{
    [Key]
    public byte Id { get; set; }

    [StringLength(50)]
    public string Nombre { get; set; } = null!;

    public TimeOnly Duracion { get; set; }

    [InverseProperty("IdTipoServicioNavigation")]
    public virtual ICollection<Servicio> Servicios { get; set; } = new List<Servicio>();
}