namespace BaseReservation.Application.ResponseDTOs;

public record ResponseReservationDetailDto
{
    public int Id { get; set; }

    public int ReservationId { get; set; }

    public byte? ServiceId { get; set; }

    public short? ProductId { get; set; }

    public virtual ResponseReservationDto Reservation { get; set; } = null!;

    public virtual ResponseServiceDto? Service { get; set; }

    public virtual ResponseProductDto? Product { get; set; }
}