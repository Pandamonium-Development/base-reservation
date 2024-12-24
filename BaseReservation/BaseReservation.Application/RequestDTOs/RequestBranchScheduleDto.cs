namespace BaseReservation.Application.RequestDTOs;

public record RequestBranchScheduleDto
{
    public short Id { get; set; }

    public byte BranchId { get; set; }

    public short ScheduleId { get; set; }
}