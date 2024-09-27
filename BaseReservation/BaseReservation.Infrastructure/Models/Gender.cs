using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseReservation.Infrastructure.Models;

[Table("Gender")]
public partial class Gender
{
    [Key]
    public byte Id { get; set; }

    [StringLength(25)]
    public string Name { get; set; } = null!;

    [InverseProperty("GenderIdNavigation")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}