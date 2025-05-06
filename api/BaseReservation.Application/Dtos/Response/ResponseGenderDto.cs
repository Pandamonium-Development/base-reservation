using BaseReservation.Application.Dtos.Response.Base;

namespace BaseReservation.Application.Dtos.Response;

public record ResponseGenderDto : BaseSimpleEntity
{
    public string Name { get; set; } = null!;

    public virtual ICollection<ResponseUserDto> Users { get; set; } = new List<ResponseUserDto>();
}