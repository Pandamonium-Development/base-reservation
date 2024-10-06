using BaseReservation.Application.ResponseDTOs.Base;

namespace BaseReservation.Application.ResponseDTOs;

public record ResponseCategoryDto : BaseEntity
{
    public byte Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<ResponseProductDto> Products { get; set; } = new List<ResponseProductDto>();
}