using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseReservation.Infrastructure.Models;

[Table("TypeService")]
public partial class TypeService
{
    [Key]
    public byte Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    public TimeOnly BaseDuration { get; set; }

    [InverseProperty("TypeServiceIdNavigation")]
    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}