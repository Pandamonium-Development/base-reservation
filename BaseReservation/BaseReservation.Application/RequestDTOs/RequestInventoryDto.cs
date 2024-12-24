using BaseReservation.Application.Enums;

namespace BaseReservation.Application.RequestDTOs;

public record RequestInventoryDto : RequestBaseDto
{
    public short Id { get; set; }

    public string Name { get; set; } = null!;

    public byte BranchId { get; set; }

    public TypeInventory TypeInventory { get; set; }

    public bool Active { get; set; }
}