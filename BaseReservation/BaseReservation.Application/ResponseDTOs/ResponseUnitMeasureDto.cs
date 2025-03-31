using BaseReservation.Application.ResponseDTOs.Base;

namespace BaseReservation.Application.ResponseDTOs;

public record ResponseUnitMeasureDto : BaseSimpleEntity
{
    public string Name { get; set; } = null!;

    public string Symbol { get; set; } = null!;

    public virtual ICollection<ResponseProductDto> Products { get; set; } = new List<ResponseProductDto>();
}