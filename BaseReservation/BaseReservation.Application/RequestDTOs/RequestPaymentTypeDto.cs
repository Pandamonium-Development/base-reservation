namespace BaseReservation.Application.RequestDTOs;

public record RequestPaymentTypeDto
{
    public byte Id { get; set; }

    public string Description { get; set; } = null!;

    public int ReferenceNumber { get; set; }
}