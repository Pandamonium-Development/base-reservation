namespace BaseReservation.Application.RequestDTOs;

public record RequestReservationQuestionDto : RequestBaseDto
{
    public int Id { get; set; }

    public int ReservationId { get; set; }

    public string Question { get; set; } = null!;

    public bool Active { get; set; }

    public string? Answer { get; set; }
}