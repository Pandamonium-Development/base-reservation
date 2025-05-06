using Microsoft.EntityFrameworkCore;
using BaseReservation.Domain.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseReservation.Infrastructure;

[Table("BranchScheduleBlock")]
[Index("BranchScheduleId", Name = "IX_BranchScheduleBlock_BranchSchedule")]
public partial class BranchScheduleBlock : BaseSimpleDto
{
    public long BranchScheduleId { get; set; }

    public TimeOnly StartHour { get; set; }

    public TimeOnly EndHour { get; set; }

    public bool Active { get; set; }

    [ForeignKey("BranchScheduleId")]
    [InverseProperty("BranchScheduleBlocks")]
    public virtual BranchSchedule BranchScheduleIdNavigation { get; set; } = null!;
}