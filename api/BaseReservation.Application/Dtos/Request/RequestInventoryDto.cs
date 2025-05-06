using BaseReservation.Application.Enums;

namespace BaseReservation.Application.Dtos.Request;

public record RequestInventoryDto : RequestBaseDto
{
    public string Name { get; set; } = null!;

    public long BranchId { get; set; }

    public TypeInventoryApplication TypeInventory { get; set; }
}