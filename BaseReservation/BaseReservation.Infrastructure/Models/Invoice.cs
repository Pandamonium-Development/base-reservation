﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Models;

[Table("Invoice")]
[Index("CustomerId", Name = "IX_Invoice_CustomerId")]
[Index("TaxId", Name = "IX_Invoice_TaxId")]
[Index("OrderId", Name = "IX_Invoice_OrderId")]
[Index("BranchId", Name = "IX_Invoice_BranchId")]
[Index("PaymentTypeId", Name = "IX_Invoice_PaymentTypeId")]
public partial class Invoice : BaseModel
{
    [Key]
    public long Id { get; set; }

    public byte BranchId { get; set; }

    public long? OrderId { get; set; }

    public short CustomerId { get; set; }

    [StringLength(160)]
    public string CustomerName { get; set; } = null!;

    public DateOnly Date { get; set; }

    public byte PaymentTypeId { get; set; }

    public short Number { get; set; }

    public byte TaxId { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal TaxRate { get; set; }

    [Column(TypeName = "money")]
    public decimal SubTotal { get; set; }

    [Column(TypeName = "money")]
    public decimal Tax { get; set; }

    [Column(TypeName = "money")]
    public decimal Total { get; set; }

    [InverseProperty("InvoiceIdNavigation")]
    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();

    [ForeignKey("CustomerId")]
    [InverseProperty("Invoices")]
    public virtual Customer CustomerIdNavigation { get; set; } = null!;

    [ForeignKey("TaxId")]
    [InverseProperty("Invoices")]
    public virtual Tax TaxIdNavigation { get; set; } = null!;

    [ForeignKey("OrderId")]
    [InverseProperty("Invoices")]
    public virtual Order? OrderIdNavigation { get; set; }

    [ForeignKey("BranchId")]
    [InverseProperty("Invoices")]
    public virtual Branch BranchIdNavigation { get; set; } = null!;

    [ForeignKey("PaymentTypeId")]
    [InverseProperty("Invoices")]
    public virtual PaymentType PaymentTypeIdNavigation { get; set; } = null!;
}