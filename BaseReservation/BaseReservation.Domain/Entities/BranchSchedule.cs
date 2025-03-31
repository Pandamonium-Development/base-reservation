using Microsoft.EntityFrameworkCore;
using BaseReservation.Domain.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseReservation.Infrastructure;

[Table("BranchSchedule")]
[Index("ScheduleId", Name = "IX_BranchSchedule_ScheduleId")]
[Index("BranchId", Name = "IX_BranchSchedule_BranchId")]
public partial class BranchSchedule : BaseSimpleDto
{
    public long BranchId { get; set; }

    public long ScheduleId { get; set; }

    [ForeignKey("ScheduleId")]
    [InverseProperty("BranchSchedules")]
    public virtual Schedule ScheduleIdNavigation { get; set; } = null!;

    [ForeignKey("BranchId")]
    [InverseProperty("BranchSchedules")]
    public virtual Branch BranchIdNavigation { get; set; } = null!;

    [InverseProperty("BranchScheduleIdNavigation")]
    public virtual ICollection<BranchScheduleBlock> BranchScheduleBlocks { get; set; } = new List<BranchScheduleBlock>();
}