namespace BaseReservation.Application.RequestDTOs;

public record RequestReservationDetailDto
{
    public int Id { get; set; }

    public int ReservationId { get; set; }

    public byte? ServiceId { get; set; }

    public short? ProductId { get; set; }
}