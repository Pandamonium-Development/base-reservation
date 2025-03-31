using BaseReservation.Domain.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseReservation.Infrastructure;

[Table("Province")]
public partial class Province : BaseSimpleDto
{
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [InverseProperty("ProvinceIdNavigation")]
    public virtual ICollection<Canton> Cantons { get; set; } = new List<Canton>();
}