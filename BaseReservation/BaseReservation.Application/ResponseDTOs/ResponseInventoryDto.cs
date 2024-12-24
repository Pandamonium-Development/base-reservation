using BaseReservation.Application.Enums;
using BaseReservation.Application.ResponseDTOs.Base;

namespace BaseReservation.Application.ResponseDTOs;

public record ResponseInventoryDto : BaseEntity
{
    public short Id { get; set; }

    public string Name { get; set; } = null!;

    public byte IdSucursal { get; set; }

    public TypeInventory TypeInventory { get; set; }

    public bool Active { get; set; }

    public virtual ResponseBranchDto Branch { get; set; } = null!;

    public virtual ICollection<ResponseInventoryProductDto> InventoryProducts { get; set; } = new List<ResponseInventoryProductDto>();
}