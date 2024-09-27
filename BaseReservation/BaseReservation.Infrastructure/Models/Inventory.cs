using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseReservation.Infrastructure.Enums;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Models;

[Table("Inventory")]
[Index("BranchId", Name = "IX_Inventory_BranchId")]
public partial class Inventory : BaseModel
{
    [Key]
    public short Id { get; set; }

    public byte BranchId { get; set; }

    public string Name { get; set; } = null!;

    public TypeInventory TypeInventory { get; set; }

    public bool Active { get; set; }

    [ForeignKey("BranchId")]
    [InverseProperty("Inventaries")]
    public virtual Branch BranchIdNavigation { get; set; } = null!;

    [InverseProperty("InventaryIdNavigation")]
    public virtual ICollection<InventoryProduct> InventoryProducts { get; set; } = new List<InventoryProduct>();
}