using BaseReservation.Domain.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseReservation.Infrastructure;

[Table("UnitMeasure")]
public partial class UnitMeasure : BaseSimpleDto
{
    [StringLength(25)]
    public string Name { get; set; } = null!;

    [StringLength(5)]
    public string Symbol { get; set; } = null!;

    [InverseProperty("UnitMeasureIdNavigation")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}