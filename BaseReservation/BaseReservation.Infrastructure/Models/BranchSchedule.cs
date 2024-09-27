using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Models;

[Table("BranchSchedule")]
[Index("ScheduleId", Name = "IX_BranchSchedule_ScheduleId")]
[Index("BranchId", Name = "IX_BranchSchedule_BranchId")]
public partial class BranchSchedule
{
    [Key]
    public short Id { get; set; }

    public byte BranchId { get; set; }

    public short ScheduleId { get; set; }

    [ForeignKey("ScheduleId")]
    [InverseProperty("BranchSchedules")]
    public virtual Schedule ScheduleIdNavigation { get; set; } = null!;

    [ForeignKey("BranchId")]
    [InverseProperty("BranchSchedules")]
    public virtual Branch BranchIdNavigation { get; set; } = null!;

    [InverseProperty("BranchScheduleIdNavigation")]
    public virtual ICollection<BranchScheduleBlock> BranchScheduleBlocks { get; set; } = new List<BranchScheduleBlock>();
}