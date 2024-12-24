namespace BaseReservation.Application.RequestDTOs;

public class RequestBranchScheduleBlockDto
{
    public long Id { get; set; }

    public short BranchScheduleId { get; set; }

    public TimeOnly StartHour { get; set; }

    public TimeOnly EndHour { get; set; }

    public bool Active { get; set; }
}