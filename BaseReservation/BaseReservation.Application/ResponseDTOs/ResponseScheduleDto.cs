using BaseReservation.Application.Enums;
using BaseReservation.Application.ResponseDTOs.Base;

namespace BaseReservation.Application.ResponseDTOs;

public record ResponseScheduleDto : BaseEntity
{
    public short Id { get; set; }

    public WeekDay Day { get; set; }

    public TimeOnly StartHour { get; set; }

    public TimeOnly EndHour { get; set; }

    public virtual ICollection<ResponseBranchScheduleDto> BranchSchedules { get; set; } = new List<ResponseBranchScheduleDto>();
}