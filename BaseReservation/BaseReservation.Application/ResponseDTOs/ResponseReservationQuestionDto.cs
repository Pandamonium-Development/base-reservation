using BaseReservation.Application.ResponseDTOs.Base;

namespace BaseReservation.Application.ResponseDTOs;

public record ResponseReservationQuestionDto : BaseEntity
{
    public long ReservationId { get; set; }

    public string Question { get; set; } = null!;

    public string? Answer { get; set; }

    public virtual ResponseReservationDto Reservation { get; set; } = null!;
}