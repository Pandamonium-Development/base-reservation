using Microsoft.EntityFrameworkCore;
using BaseReservation.Domain.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseReservation.Infrastructure;

[Table("InvoiceDetailProduct")]
[Index("InvoiceDetailId", Name = "IX_InvoiceDetailProduct_InvoiceDetailId")]
[Index("ProductId", Name = "IX_InvoiceDetailProduct_ProductId")]
public partial class InvoiceDetailProduct : BaseSimpleDto
{
    public long InvoiceDetailId { get; set; }

    public long ProductId { get; set; }

    [Column(TypeName = "decimal(6, 2)")]
    public decimal Quantity { get; set; }

    [ForeignKey("InvoiceDetailId")]
    [InverseProperty("InvoiceDetailProducts")]
    public virtual InvoiceDetail InvoiceDetailIdNavigation { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("InvoiceDetailProducts")]
    public virtual Product ProductIdNavigation { get; set; } = null!;
}