using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Models;

[Table("Order")]
[Index("CustomerId", Name = "IX_Order_CustomerId")]
[Index("TaxId", Name = "IX_Order_TaxId")]
[Index("ReservationId", Name = "IX_Order_ReservationId")]
[Index("BranchId", Name = "IX_Order_BranchId")]
[Index("PaymentTypeId", Name = "IX_Order_PaymentTypeId")]
public partial class Order : BaseModel
{
    [Key]
    public long Id { get; set; }

    public byte BranchId { get; set; }

    public int ReservationId { get; set; }

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

    public byte StatusOrderId { get; set; }

    [InverseProperty("OrderIdNavigation")]
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    [InverseProperty("OrderIdNavigation")]
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    [ForeignKey("CustomerId")]
    [InverseProperty("Orders")]
    public virtual Customer CustomerIdNavigation { get; set; } = null!;

    [ForeignKey("StatusOrderId")]
    [InverseProperty("Orders")]
    public virtual StatusOrder StatusOrderIdNavigation { get; set; } = null!;

    [ForeignKey("TaxId")]
    [InverseProperty("Orders")]
    public virtual Tax TaxIdNavigation { get; set; } = null!;

    [ForeignKey("ReservationId")]
    [InverseProperty("Orders")]
    public virtual Reservation ReservationIdNavigation { get; set; } = null!;

    [ForeignKey("BranchId")]
    [InverseProperty("Orders")]
    public virtual Branch BranchIdNavigation { get; set; } = null!;

    [ForeignKey("PaymentTypeId")]
    [InverseProperty("Orders")]
    public virtual PaymentType PaymentTypeIdNavigation { get; set; } = null!;
}