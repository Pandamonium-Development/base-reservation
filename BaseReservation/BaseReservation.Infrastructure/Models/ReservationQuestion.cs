using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Models;

[Table("ReservationQuestion")]
[Index("ReservationId", Name = "IX_ReservationQuestion_ReservationId")]
public partial class ReservationQuestion : BaseModel
{
    [Key]
    public int Id { get; set; }

    public int ReservationId { get; set; }

    [StringLength(250)]
    public string Question { get; set; } = null!;

    [StringLength(250)]
    public string? Answer { get; set; }

    public bool Active { get; set; }

    [ForeignKey("ReservationId")]
    [InverseProperty("ReservationQuestions")]
    public virtual Reservation ReservationIdNavigation { get; set; } = null!;
}