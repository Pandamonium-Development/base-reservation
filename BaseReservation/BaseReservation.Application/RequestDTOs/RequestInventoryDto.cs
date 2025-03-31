using BaseReservation.Application.Enums;

namespace BaseReservation.Application.RequestDTOs;

public record RequestInventoryDto : RequestBaseDto
{
    public string Name { get; set; } = null!;

    public long BranchId { get; set; }

    public TypeInventoryApplication TypeInventory { get; set; }
}