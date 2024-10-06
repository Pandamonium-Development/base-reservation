using System.ComponentModel;

namespace BaseReservation.Application.ResponseDTOs;

public record ResponseOrderDetailProductDto
{
    public long Id { get; set; }

    public long OrderDetailId { get; set; }

    public short ProductId { get; set; }

    public decimal Quantity { get; set; }

    public virtual ResponseOrderDetailDto OrderDetail { get; set; } = null!;

    public virtual ResponseProductDto Product { get; set; } = null!;
}