namespace BaseReservation.Application.RequestDTOs;

public class RequestBranchScheduleBlockDto
{
    public long BranchScheduleId { get; set; }

    public TimeOnly StartHour { get; set; }

    public TimeOnly EndHour { get; set; }

    public bool Active { get; set; }
}