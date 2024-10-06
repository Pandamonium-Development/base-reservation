using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Models;

[Table("ReservationDetail")]
[Index("ReservationId", Name = "IX_ReservationDetail_ReservationId")]
[Index("ServiceId", Name = "IX_ReservationDetail_ServiceId")]
public partial class ReservationDetail
{
    [Key]
    public int Id { get; set; }

    public int ReservationId { get; set; }

    public byte? ServiceId { get; set; }

    public short? ProductId { get; set; }

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