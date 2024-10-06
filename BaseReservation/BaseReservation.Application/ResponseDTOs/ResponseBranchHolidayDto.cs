namespace BaseReservation.Application.ResponseDTOs;

public record ResponseBranchHolidayDto
{
    public short Id { get; set; }

    public byte HolidayId { get; set; }

    public byte BranchId { get; set; }

    public DateOnly Date { get; set; }

    public short Year { get; set; }

    public virtual ResponseHolidayDto Holiday { get; set; } = null!;

    public virtual ResponseBranchDto Branch { get; set; } = null!;
}