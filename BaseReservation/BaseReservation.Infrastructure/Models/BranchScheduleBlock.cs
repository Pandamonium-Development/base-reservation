using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Models;

[Table("BranchScheduleBlock")]
[Index("BranchScheduleId", Name = "IX_BranchScheduleBlock_BranchSchedule")]
public partial class BranchScheduleBlock
{
    [Key]
    public long Id { get; set; }

    public short BranchScheduleId { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public bool Active { get; set; }

    [ForeignKey("BranchScheduleId")]
    [InverseProperty("BranchScheduleBlocks")]
    public virtual BranchSchedule BranchScheduleIdNavigation { get; set; } = null!;
}