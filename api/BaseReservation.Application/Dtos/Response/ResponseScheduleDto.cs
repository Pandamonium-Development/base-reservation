using BaseReservation.Application.Enums;
using BaseReservation.Application.Dtos.Response.Base;

namespace BaseReservation.Application.Dtos.Response;

public record ResponseScheduleDto : BaseEntity
{
    public WeekDayApplication Day { get; set; }

    public TimeOnly StartHour { get; set; }

    public TimeOnly EndHour { get; set; }

    public virtual ICollection<ResponseBranchScheduleDto> BranchSchedules { get; set; } = new List<ResponseBranchScheduleDto>();
}