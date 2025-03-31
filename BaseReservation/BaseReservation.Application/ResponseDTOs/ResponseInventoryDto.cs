using BaseReservation.Application.Enums;
using BaseReservation.Application.ResponseDTOs.Base;

namespace BaseReservation.Application.ResponseDTOs;

public record ResponseInventoryDto : BaseEntity
{
    public string Name { get; set; } = null!;

    public long BranchId { get; set; }

    public TypeInventoryApplication TypeInventory { get; set; }

    public virtual ResponseBranchDto Branch { get; set; } = null!;

    public virtual ICollection<ResponseInventoryProductDto> InventoryProducts { get; set; } = new List<ResponseInventoryProductDto>();
}