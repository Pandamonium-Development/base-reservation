using BaseReservation.Application.ResponseDTOs.Base;

namespace BaseReservation.Application.ResponseDTOs;

public record ResponseOrderDto : BaseEntity
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

    public byte StatusOrderId { get; set; }

    public byte BranchId { get; set; }

    public virtual ICollection<ResponseOrderDetailDto> OrderDetails { get; set; } = new List<ResponseOrderDetailDto>();

    public virtual ResponseCustomerDto Customer { get; set; } = null!;

    public virtual ResponseTaxDto TaxInfo { get; set; } = null!;

    public virtual ResponsePaymentTypeDto PaymentType { get; set; } = null!;

    public virtual ResponseReservationDto Reservation { get; set; } = null!;

    public virtual ResponseBranchDto Branch { get; set; } = null!;
}