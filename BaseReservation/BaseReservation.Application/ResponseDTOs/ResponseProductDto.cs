using BaseReservation.Application.ResponseDTOs.Base;

namespace BaseReservation.Application.ResponseDTOs;

public record ResponseProductDto : BaseEntity
{
    public short Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Brand { get; set; } = null!;

    public byte CategoryId { get; set; }

    public decimal Price { get; set; }

    public string Sku { get; set; } = null!;

    public byte unitMeasure { get; set; }

    public bool Active { get; set; }

    public virtual ICollection<ResponseInvoiceDetailProductDto> InvoiceDetailProducts { get; set; } = new List<ResponseInvoiceDetailProductDto>();

    public virtual ResponseCategoryDto Category { get; set; } = null!;

    public virtual ResponseUnitMeasureDto UnitMeasure { get; set; } = null!;

    public virtual ICollection<ResponseInventoryDto> Inventarios { get; set; } = new List<ResponseInventoryDto>();

    public virtual ICollection<ResponseInventoryProductDto> InventoryProducts { get; set; } = new List<ResponseInventoryProductDto>();
}