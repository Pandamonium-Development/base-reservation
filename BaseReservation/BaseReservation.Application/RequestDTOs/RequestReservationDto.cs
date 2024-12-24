namespace BaseReservation.Application.RequestDTOs;

public record RequestReservationDto : RequestBaseDto
{
    public int Id { get; set; }

    public DateOnly Date { get; set; }

    public TimeOnly Hour { get; set; }

    public byte BranchId { get; set; }

    public short CustomerId { get; set; }

    public string CustomerName { get; set; } = null!;

    public string Status { get; set; } = null!;

    public bool Active { get; set; }

    public List<RequestReservationQuestionDto> ReservationQuestion { get; set; } = null!;

    public List<RequestReservationDetailDto> ReservationDetails { get; set; } = null!;
}