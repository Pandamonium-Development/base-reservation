using BaseReservation.Application.ResponseDTOs.Base;

namespace BaseReservation.Application.ResponseDTOs;

public record ResponseRolDto : BaseEntity
{
    public byte Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public string Tipo { get; set; } = null!;

    public bool Activo { get; set; }

    public virtual ICollection<ResponseUsuarioDto> Usuarios { get; set; } = new List<ResponseUsuarioDto>();
}