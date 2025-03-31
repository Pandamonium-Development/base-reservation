using BaseReservation.Application.ResponseDTOs.Base;

namespace BaseReservation.Application.ResponseDTOs;

public record ResponseCantonDto : BaseSimpleEntity
{
    public string Name { get; set; } = null!;

    public long ProvinceId { get; set; }

    public virtual ICollection<ResponseDistrictDto> Districts { get; set; } = new List<ResponseDistrictDto>();

    public virtual ResponseProvinceDto Province { get; set; } = null!;
}