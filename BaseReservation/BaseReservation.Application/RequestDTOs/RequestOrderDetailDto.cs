namespace BaseReservation.Application.RequestDTOs;

public record RequestOrderDetailDto
{
    public long Id { get; set; }

    public long OrderId { get; set; }

    public byte? ServiceId { get; set; }

    public short? ProductId { get; set; }

    public byte LineNumber { get; set; }

    public short Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal SubTotal { get; set; }

    public decimal Tax { get; set; }

    public decimal Total { get; set; }
}