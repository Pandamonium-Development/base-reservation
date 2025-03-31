using BaseReservation.Domain.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseReservation.Infrastructure;

[Table("Role")]
public partial class Role : BaseEntity
{
    [StringLength(50)]
    public string Description { get; set; } = null!;

    [StringLength(50)]
    public string Type { get; set; } = null!;

    [InverseProperty("RoleIdNavigation")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}