using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseReservation.Infrastructure.Enums;

namespace BaseReservation.Infrastructure.Models;

[Table("Schedule")]
public partial class Schedule : BaseModel
{
    [Key]
    public short Id { get; set; }

    public WeekDay Day { get; set; }

    public TimeOnly StartHour { get; set; }

    public TimeOnly EndHour { get; set; }

    public bool Active { get; set; }

    [InverseProperty("ScheduleIdNavigation")]
    public virtual ICollection<BranchSchedule> BranchSchedules { get; set; } = new List<BranchSchedule>();
}