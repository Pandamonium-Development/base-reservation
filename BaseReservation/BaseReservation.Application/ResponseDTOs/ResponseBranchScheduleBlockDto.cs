namespace BaseReservation.Application.ResponseDTOs;

public record ResponseBranchScheduleBlockDto
{
    public long Id { get; set; }

    public short BranchScheduleId { get; set; }

    public TimeOnly StartHour { get; set; }

    public TimeOnly EndHour { get; set; }

    public bool Active { get; set; }

    public virtual ResponseBranchScheduleDto BranchSchedule { get; set; } = null!;
}