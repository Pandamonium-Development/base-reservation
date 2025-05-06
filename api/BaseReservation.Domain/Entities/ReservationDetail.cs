using Microsoft.EntityFrameworkCore;
using BaseReservation.Domain.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseReservation.Infrastructure;

[Table("ReservationDetail")]
[Index("ReservationId", Name = "IX_ReservationDetail_ReservationId")]
[Index("ServiceId", Name = "IX_ReservationDetail_ServiceId")]
public partial class ReservationDetail : BaseSimpleDto
{
    public long ReservationId { get; set; }

    public long? ServiceId { get; set; }

    public long? ProductId { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("ReservationDetails")]
    public virtual Product? ProductIdNavigation { get; set; }

    [ForeignKey("ReservationId")]
    [InverseProperty("ReservationDetails")]
    public virtual Reservation ReservationIdNavigation { get; set; } = null!;

    [ForeignKey("ServiceId")]
    [InverseProperty("ReservationDetails")]
    public virtual Service? ServiceIdNavigation { get; set; }
}