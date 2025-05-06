using BaseReservation.Application.Enums;
using BaseReservation.Application.Dtos.Response.Base;

namespace BaseReservation.Application.Dtos.Response;

public record ResponseInventoryProductTransactionDto : BaseEntity
{
    public long InventoryProductId { get; set; }

    public TransactionTypeInventoryApplication TransactionType { get; set; }

    public decimal Quantity { get; set; }

    public virtual ResponseInventoryProductDto InventoryProduct { get; set; } = null!;
}