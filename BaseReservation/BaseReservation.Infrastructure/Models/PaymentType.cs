using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseReservation.Infrastructure.Models;

[Table("PaymentType")]
public partial class PaymentType
{
    [Key]
    public byte Id { get; set; }

    [StringLength(50)]
    public string Description { get; set; } = null!;

    [StringLength(50)]
    public string ReferenceNumber { get; set; } = null!;

    [InverseProperty("PaymentTypeIdNavigation")]
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    [InverseProperty("PaymentTypeIdNavigation")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}