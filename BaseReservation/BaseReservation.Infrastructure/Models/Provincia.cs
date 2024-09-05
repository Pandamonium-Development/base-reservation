using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseReservation.Infrastructure.Models;

[Table("Provincia")]
public partial class Provincia
{
    [Key]
    public byte Id { get; set; }

    [StringLength(50)]
    public string Nombre { get; set; } = null!;

    [InverseProperty("IdProvinciaNavigation")]
    public virtual ICollection<Canton> Cantons { get; set; } = new List<Canton>();
}