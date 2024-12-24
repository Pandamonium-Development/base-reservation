namespace BaseReservation.Application.RequestDTOs;

public record RequestOrderDto : RequestBaseDto
{
    public long Id { get; set; }

    public short CustomerId { get; set; }

    public string CustomerName { get; set; } = null!;

    public DateOnly Date { get; set; }

    public byte PaymentTypeId { get; set; }

    public short Number { get; set; }

    public byte TaxId { get; set; }

    public int ReservationId { get; set; }

    public decimal TaxRate { get; set; }

    public decimal SubTotal { get; set; }

    public decimal Tax { get; set; }

    public decimal Total { get; set; }

    public char StatusOrderId { get; set; }

    public byte BranchId { get; set; }

    public IEnumerable<RequestOrderDetailDto> OrderDetails { get; set; } = null!;
}