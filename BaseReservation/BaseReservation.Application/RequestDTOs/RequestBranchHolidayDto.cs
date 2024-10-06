namespace BaseReservation.Application.RequestDTOs;

public record RequestBranchHolidayDto
{
    public short Id { get; set; }

    public byte HolidayId { get; set; }

    public byte BranchId { get; set; }

    public DateOnly Date { get; set; }

    public short Year { get; set; }
}