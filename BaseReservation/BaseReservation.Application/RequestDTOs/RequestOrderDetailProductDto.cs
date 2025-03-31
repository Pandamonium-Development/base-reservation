namespace BaseReservation.Application.RequestDTOs;

public record RequestOrderDetailProductDto : RequestBaseDto
{
    public long OrderDetailId { get; set; }

    public long ProductId { get; set; }

    public decimal Quantity { get; set; }
}