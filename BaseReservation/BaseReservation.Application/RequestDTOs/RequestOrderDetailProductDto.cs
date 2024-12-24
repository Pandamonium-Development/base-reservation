namespace BaseReservation.Application.RequestDTOs;

public record RequestOrderDetailProductDto
{
    public long Id { get; set; }

    public long OrderDetailId { get; set; }

    public short ProductId { get; set; }

    public decimal Quantity { get; set; }
}