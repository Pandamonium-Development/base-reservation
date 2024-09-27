using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Models;

[Table("InventoryProduct")]
[Index("InventoryId", Name = "IX_InventoryProduct_InventoryId")]
[Index("ProductId", Name = "IX_InventoryProduct_ProductId")]
public partial class InventoryProduct : BaseModel
{
    [Key]
    public long Id { get; set; }

    public short InventoryId { get; set; }

    public short ProductId { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Assignable { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Mininum { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Maximum { get; set; }

    [ForeignKey("InventoryId")]
    [InverseProperty("InventoryProducts")]
    public virtual Inventory InventoryIdNavigation { get; set; } = null!;

    [ForeignKey("IdProducto")]
    [InverseProperty("InventoryProducts")]
    public virtual Product ProductIdNavigation { get; set; } = null!;

    [InverseProperty("InventoryProductIdNavigation")]
    public virtual ICollection<InventoryProductTransaction> InventoryProductTransactions { get; set; } = new List<InventoryProductTransaction>();
}