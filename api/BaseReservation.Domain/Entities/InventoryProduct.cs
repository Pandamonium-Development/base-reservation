using Microsoft.EntityFrameworkCore;
using BaseReservation.Domain.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseReservation.Infrastructure;

[Table("InventoryProduct")]
[Index("InventoryId", Name = "IX_InventoryProduct_InventoryId")]
[Index("ProductId", Name = "IX_InventoryProduct_ProductId")]
public partial class InventoryProduct : BaseEntity
{
    public long InventoryId { get; set; }

    public long ProductId { get; set; }

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