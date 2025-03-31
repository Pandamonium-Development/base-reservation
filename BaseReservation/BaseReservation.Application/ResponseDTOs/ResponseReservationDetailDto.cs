using BaseReservation.Application.ResponseDTOs.Base;

namespace BaseReservation.Application.ResponseDTOs;

public record ResponseReservationDetailDto : BaseSimpleEntity
{
    public long ReservationId { get; set; }

    public long? ServiceId { get; set; }

    public long? ProductId { get; set; }

    public virtual ResponseReservationDto Reservation { get; set; } = null!;

    public virtual ResponseServiceDto? Service { get; set; }

    public virtual ResponseProductDto? Product { get; set; }
}