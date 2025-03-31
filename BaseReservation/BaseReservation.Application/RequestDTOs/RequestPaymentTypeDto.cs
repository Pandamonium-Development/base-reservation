namespace BaseReservation.Application.RequestDTOs;

public record RequestPaymentTypeDto : RequestBaseDto
{
    public string Description { get; set; } = null!;

    public int ReferenceNumber { get; set; }
}