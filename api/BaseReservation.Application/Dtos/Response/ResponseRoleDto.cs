using BaseReservation.Application.Dtos.Response.Base;

namespace BaseReservation.Application.Dtos.Response;

public record ResponseRoleDto : BaseEntity
{
    public string Description { get; set; } = null!;

    public string Type { get; set; } = null!;

    public virtual ICollection<ResponseUserDto> Users { get; set; } = new List<ResponseUserDto>();
}