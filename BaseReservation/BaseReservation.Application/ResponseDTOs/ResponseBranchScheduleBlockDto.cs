using BaseReservation.Application.ResponseDTOs.Base;

namespace BaseReservation.Application.ResponseDTOs;

public record ResponseBranchScheduleBlockDto : BaseSimpleEntity
{
    public long BranchScheduleId { get; set; }

    public TimeOnly StartHour { get; set; }

    public TimeOnly EndHour { get; set; }

    public bool Active { get; set; }

    public virtual ResponseBranchScheduleDto BranchSchedule { get; set; } = null!;
}