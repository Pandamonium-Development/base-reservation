using BaseReservation.Application.ResponseDTOs.Base;

namespace BaseReservation.Application.ResponseDTOs;

public record ResponseInventoryProductDto : BaseEntity
{
    public long Id { get; set; }

    public short InventoryId { get; set; }

    public short ProductId { get; set; }

    public decimal Assignable { get; set; }

    public decimal Minimum { get; set; }

    public decimal Maximum { get; set; }

    public virtual ResponseInventoryDto Inventory { get; set; } = null!;

    public virtual ResponseProductDto Product { get; set; } = null!;
}