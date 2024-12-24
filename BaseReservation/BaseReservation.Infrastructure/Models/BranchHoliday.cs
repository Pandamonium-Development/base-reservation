using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Models;

[Table("BranchHoliday")]
[Index("HolidayId", Name = "IX_BranchHoliday_HolidayId")]
[Index("BranchId", Name = "IX_BranchHoliday_BranchId")]
public partial class BranchHoliday
{
    [Key]
    public short Id { get; set; }

    public byte HolidayId { get; set; }

    public byte BranchId { get; set; }

    public short Year { get; set; }

    public DateOnly Date { get; set; }

    [ForeignKey("HolidayId")]
    [InverseProperty("BranchHolidays")]
    public virtual Holiday HolidayIdNavigation { get; set; } = null!;

    [ForeignKey("BranchId")]
    [InverseProperty("BranchHolidays")]
    public virtual Branch BranchIdNavigation { get; set; } = null!;
}