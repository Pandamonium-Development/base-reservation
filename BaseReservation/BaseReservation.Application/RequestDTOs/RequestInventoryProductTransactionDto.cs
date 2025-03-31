using BaseReservation.Application.Enums;

namespace BaseReservation.Application.RequestDTOs;

public record RequestInventoryProductTransactionDto : RequestBaseDto
{
    public long InventoryProductId { get; set; }

    public TransactionTypeInventoryApplication TransactionType { get; set; }

    public decimal Quantity { get; set; }
}