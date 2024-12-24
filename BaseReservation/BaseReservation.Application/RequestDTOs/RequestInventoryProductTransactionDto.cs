using BaseReservation.Application.Enums;

namespace BaseReservation.Application.RequestDTOs;

public record RequestInventoryProductTransactionDto : RequestBaseDto
{
    public long Id { get; set; }

    public long InventoryProductId { get; set; }

    public TransactionTypeInventory TransactionType { get; set; }

    public decimal Quantity { get; set; }
}