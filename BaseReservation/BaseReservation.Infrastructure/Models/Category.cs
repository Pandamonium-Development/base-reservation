using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseReservation.Infrastructure.Models;

[Table("Category")]
public partial class Category : BaseModel
{
    [Key]
    public byte Id { get; set; }

    [StringLength(50)]
    public string Code { get; set; } = null!;

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [InverseProperty("CategoriaIdNavigation")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}