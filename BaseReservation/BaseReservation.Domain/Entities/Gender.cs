using BaseReservation.Domain.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseReservation.Infrastructure;

[Table("Gender")]
public partial class Gender : BaseSimpleDto
{
    [StringLength(25)]
    public string Name { get; set; } = null!;

    [InverseProperty("GenderIdNavigation")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}