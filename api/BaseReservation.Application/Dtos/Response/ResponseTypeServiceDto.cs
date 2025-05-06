using BaseReservation.Application.Dtos.Response.Base;

namespace BaseReservation.Application.Dtos.Response;

public record ResponseTypeServiceDto : BaseSimpleEntity
{
    public string Name { get; set; } = null!;

    public TimeOnly Duration { get; set; }

    public virtual ICollection<ResponseServiceDto> Services { get; set; } = new List<ResponseServiceDto>();
}