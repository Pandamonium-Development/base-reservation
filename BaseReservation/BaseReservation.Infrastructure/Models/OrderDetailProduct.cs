using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Models;

[Table("OrderDetailProduct")]
[Index("OrderDetailId", Name = "IX_OrderDetailProduct_OrderDetailId")]
[Index("ProductId", Name = "IX_OrderDetailProduct_ProductId")]
public partial class OrderDetailProduct
{
    [Key]
    public long Id { get; set; }

    public long OrderDetailId { get; set; }

    public short ProductId { get; set; }

    [Column(TypeName = "decimal(6, 2)")]
    public decimal Quantity { get; set; }

    [ForeignKey("OrderDetailId")]
    [InverseProperty("OrderDetailProducts")]
    public virtual OrderDetail OrderDetailIdNavigation { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("OrderDetailProducts")]
    public virtual Product ProductIdNavigation { get; set; } = null!;
}