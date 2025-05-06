using BaseReservation.Application.Dtos.Response.Base;

namespace BaseReservation.Application.Dtos.Response;

public record ResponseProvinceDto : BaseSimpleEntity
{
    public string Name { get; set; } = null!;

    public virtual ICollection<ResponseCantonDto> Cantons { get; set; } = new List<ResponseCantonDto>();
}