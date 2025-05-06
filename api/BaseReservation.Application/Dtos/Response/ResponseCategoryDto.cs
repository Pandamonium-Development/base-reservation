using BaseReservation.Application.Dtos.Response.Base;

namespace BaseReservation.Application.Dtos.Response;

public record ResponseCategoryDto : BaseEntity
{
    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<ResponseProductDto> Products { get; set; } = new List<ResponseProductDto>();
}