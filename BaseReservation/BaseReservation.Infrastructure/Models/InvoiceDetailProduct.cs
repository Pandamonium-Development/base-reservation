using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Models;

[Table("InvoiceDetailProduct")]
[Index("InvoiceDetailId", Name = "IX_InvoiceDetailProduct_InvoiceDetailId")]
[Index("ProductId", Name = "IX_InvoiceDetailProduct_ProductId")]
public partial class InvoiceDetailProduct
{
    [Key]
    public long Id { get; set; }

    public long InvoiceDetailId { get; set; }

    public short ProductId { get; set; }

    [Column(TypeName = "decimal(6, 2)")]
    public decimal Quantity { get; set; }

    [ForeignKey("InvoiceDetailId")]
    [InverseProperty("InvoiceDetailProducts")]
    public virtual InvoiceDetail InvoiceDetailIdNavigation { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("InvoiceDetailProducts")]
    public virtual Product ProductIdNavigation { get; set; } = null!;
}