using System.ComponentModel;
using BaseReservation.Application.ResponseDTOs.Base;

namespace BaseReservation.Application.ResponseDTOs;

public record ResponseOrderDetailProductDto : BaseSimpleEntity
{
    public long OrderDetailId { get; set; }

    public long ProductId { get; set; }

    public decimal Quantity { get; set; }

    public virtual ResponseOrderDetailDto OrderDetail { get; set; } = null!;

    public virtual ResponseProductDto Product { get; set; } = null!;
}