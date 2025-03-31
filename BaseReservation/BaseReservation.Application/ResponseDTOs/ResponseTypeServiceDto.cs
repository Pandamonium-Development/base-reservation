using BaseReservation.Application.ResponseDTOs.Base;

namespace BaseReservation.Application.ResponseDTOs;

public record ResponseTypeServiceDto : BaseSimpleEntity
{
    public string Name { get; set; } = null!;

    public TimeOnly Duration { get; set; }

    public virtual ICollection<ResponseServiceDto> Services { get; set; } = new List<ResponseServiceDto>();
}