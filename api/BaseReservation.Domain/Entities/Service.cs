using Microsoft.EntityFrameworkCore;
using BaseReservation.Domain.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseReservation.Infrastructure;

[Table("Service")]
[Index("TypeServiceId", Name = "IX_Service_TypeServiceId")]
public partial class Service : BaseEntity
{
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [StringLength(150)]
    public string Description { get; set; } = null!;

    public long TypeServiceId { get; set; }

    [Column(TypeName = "money")]
    public decimal Price { get; set; }

    [StringLength(250)]
    public string? Observation { get; set; }

    [InverseProperty("ServiceIdNavigation")]
    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();

    [InverseProperty("ServiceIdNavigation")]
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    [InverseProperty("ServiceIdNavigation")]
    public virtual ICollection<ReservationDetail> ReservationDetails { get; set; } = new List<ReservationDetail>();

    [ForeignKey("TypeServiceId")]
    [InverseProperty("Services")]
    public virtual TypeService TypeServiceIdNavigation { get; set; } = null!;
}