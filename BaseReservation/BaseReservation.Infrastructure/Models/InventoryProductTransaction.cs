using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseReservation.Infrastructure.Enums;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Models;

[Table("InventoryProductTransaction")]
[Index("InventoryProductId", Name = "IX_InventoryProductTransaction_InventoryProductId")]
public partial class InventoryProductTransaction : BaseModel
{
    [Key]
    public long Id { get; set; }

    public long InventoryProductId { get; set; }

    public TransactionTypeInventory TransactionType { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Quantity { get; set; }

    [ForeignKey("InventoryProductId")]
    [InverseProperty("InventoryProductTransactions")]
    public virtual InventoryProduct InventoryProductIdNavigation { get; set; } = null!;
}