using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BaseReservation.Infrastructure.Models;

[Table("Rol")]
public partial class Rol : BaseModel
{
    [Key]
    public byte Id { get; set; }

    [StringLength(50)]
    public string Descripcion { get; set; } = null!;

    [StringLength(50)]
    public string Tipo { get; set; } = null!;

    public bool Activo { get; set; }

    [InverseProperty("IdRolNavigation")]
    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}