using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Models;

[Table("OrderDetail")]
[Index("OrderId", Name = "IX_OrderDetail_OrderId")]
[Index("ServiceId", Name = "IX_OrderDetail_ServiceId")]
public partial class OrderDetail
{
    [Key]
    public long Id { get; set; }

    public long OrderId { get; set; }

    public byte? ServiceId { get; set; }

    public short? ProductId { get; set; }

    public byte LineNumber { get; set; }

    public short Quantity { get; set; }

    [Column(TypeName = "money")]
    public decimal UnitPrice { get; set; }

    [Column(TypeName = "money")]
    public decimal SubTotal { get; set; }

    [Column(TypeName = "money")]
    public decimal Tax { get; set; }

    [Column(TypeName = "money")]
    public decimal Total { get; set; }

    [InverseProperty("OrderDetailIdNavigation")]
    public virtual ICollection<OrderDetailProduct> OrderDetailProducts { get; set; } = new List<OrderDetailProduct>();

    [ForeignKey("OrderId")]
    [InverseProperty("OrderDetails")]
    public virtual Order OrderIdNavigation { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("OrderDetails")]
    public virtual Product? ProductIdNavigation { get; set; }

    [ForeignKey("ServiceId")]
    [InverseProperty("OrderDetails")]
    public virtual Service? ServiceIdNavigation { get; set; }
}