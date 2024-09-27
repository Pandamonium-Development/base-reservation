using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseReservation.Infrastructure.Models;

[Table("StatusOrder")]
public partial class StatusOrder
{
    [Key]
    public byte Id { get; set; }

    [StringLength(25)]
    public string Description { get; set; } = null!;

    [InverseProperty("StatusOrderIdNavigation")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}