using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseReservation.Infrastructure.Models;

[Table("Categoria")]
public partial class Categoria : BaseModel
{
    [Key]
    public byte Id { get; set; }

    [StringLength(50)]
    public string Codigo { get; set; } = null!;

    [StringLength(50)]
    public string Nombre { get; set; } = null!;

    [InverseProperty("IdCategoriaNavigation")]
    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}