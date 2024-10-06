using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseReservation.Infrastructure.Models;

[Table("Province")]
public partial class Province
{
    [Key]
    public byte Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [InverseProperty("ProvinceIdNavigation")]
    public virtual ICollection<Canton> Cantons { get; set; } = new List<Canton>();
}