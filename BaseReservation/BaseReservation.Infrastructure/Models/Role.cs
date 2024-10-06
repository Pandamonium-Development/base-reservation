using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BaseReservation.Infrastructure.Models;

[Table("Role")]
public partial class Role : BaseModel
{
    [Key]
    public byte Id { get; set; }

    [StringLength(50)]
    public string Description { get; set; } = null!;

    [StringLength(50)]
    public string Type { get; set; } = null!;

    public bool Active { get; set; }

    [InverseProperty("RoleIdNavigation")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}