namespace BaseReservation.Application.RequestDTOs;

public record RequestBranchScheduleDto
{
    public long BranchId { get; set; }

    public long ScheduleId { get; set; }
}