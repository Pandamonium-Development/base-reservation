using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Models;

[Table("InvoiceDetail")]
[Index("InvoiceId", Name = "IX_InvoiceDetail_InvoiceId")]
[Index("ServiceId", Name = "IX_InvoiceDetail_ServiceId")]
public partial class InvoiceDetail
{
    [Key]
    public long Id { get; set; }

    public long InvoiceId { get; set; }

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

    [InverseProperty("InvoiceDetailIdNavigation")]
    public virtual ICollection<InvoiceDetailProduct> InvoiceDetailProducts { get; set; } = new List<InvoiceDetailProduct>();

    [ForeignKey("InvoiceId")]
    [InverseProperty("InvoiceDetails")]
    public virtual Invoice InvoiceIdNavigation { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("InvoiceDetails")]
    public virtual Product? ProductIdNavigation { get; set; }

    [ForeignKey("ServiceId")]
    [InverseProperty("InvoiceDetails")]
    public virtual Service? ServiceIdNavigation { get; set; }
}