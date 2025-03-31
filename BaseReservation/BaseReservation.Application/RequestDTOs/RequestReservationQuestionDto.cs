namespace BaseReservation.Application.RequestDTOs;

public record RequestReservationQuestionDto : RequestBaseDto
{
    public long ReservationId { get; set; }

    public string Question { get; set; } = null!;

    public string? Answer { get; set; }
}