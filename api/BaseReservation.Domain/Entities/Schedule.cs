using BaseReservation.Domain.Core.Models;
using BaseReservation.Infrastructure.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseReservation.Infrastructure;

[Table("Schedule")]
public partial class Schedule : BaseEntity
{
    public WeekDay Day { get; set; }

    public TimeOnly StartHour { get; set; }

    public TimeOnly EndHour { get; set; }

    [InverseProperty("ScheduleIdNavigation")]
    public virtual ICollection<BranchSchedule> BranchSchedules { get; set; } = new List<BranchSchedule>();
}