using BaseReservation.Application.ResponseDTOs.Base;

namespace BaseReservation.Application.ResponseDTOs;

public record ResponseReservationQuestionDto : BaseEntity
{
    public int Id { get; set; }

    public int ReservationId { get; set; }

    public string Question { get; set; } = null!;

    public bool Active { get; set; }

    public string? Answer { get; set; }

    public virtual ResponseReservationDto Reservation { get; set; } = null!;
}