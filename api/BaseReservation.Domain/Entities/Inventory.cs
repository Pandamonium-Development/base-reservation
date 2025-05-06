using Microsoft.EntityFrameworkCore;
using BaseReservation.Domain.Core.Models;
using BaseReservation.Infrastructure.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseReservation.Infrastructure;

[Table("Inventory")]
[Index("BranchId", Name = "IX_Inventory_BranchId")]
public partial class Inventory : BaseEntity
{
    public long BranchId { get; set; }

    public string Name { get; set; } = null!;

    public TypeInventory TypeInventory { get; set; }

    [ForeignKey("BranchId")]
    [InverseProperty("Inventories")]
    public virtual Branch BranchIdNavigation { get; set; } = null!;

    [InverseProperty("InventoryIdNavigation")]
    public virtual ICollection<InventoryProduct> InventoryProducts { get; set; } = new List<InventoryProduct>();
}