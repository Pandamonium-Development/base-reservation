using BaseReservation.Application.ResponseDTOs.Base;

namespace BaseReservation.Application.ResponseDTOs;

public record ResponseRoleDto : BaseEntity
{
    public byte Id { get; set; }

    public string Description { get; set; } = null!;

    public string Type { get; set; } = null!;

    public bool Active { get; set; }

    public virtual ICollection<ResponseUserDto> Users { get; set; } = new List<ResponseUserDto>();
}