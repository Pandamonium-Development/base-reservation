using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Models;

[Table("Canton")]
[Index("ProvinceId", Name = "IX_Canton_ProvinceId")]
public partial class Canton
{
    [Key]
    public byte Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    public byte ProvinceId { get; set; }

    [InverseProperty("CantonIdNavigation")]
    public virtual ICollection<District> Districts { get; set; } = new List<District>();

    [ForeignKey("ProvinceId")]
    [InverseProperty("Cantons")]
    public virtual Province ProvinceIdNavigation { get; set; } = null!;
}