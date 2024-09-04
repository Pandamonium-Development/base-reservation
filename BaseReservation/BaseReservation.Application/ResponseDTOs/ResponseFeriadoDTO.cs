using BaseReservation.Application.ResponseDTOs.Base;
using BaseReservation.Application.ResponseDTOs.Enums;

namespace BaseReservation.Application.ResponseDTOs;

public record ResponseFeriadoDTO : BaseEntity
{
    public byte Id { get; set; }

    public string Nombre { get; set; } = null!;

    public bool Activo { get; set; }

    public Mes Mes { get; set; }

    public byte Dia { get; set; }

    public virtual ICollection<ResponseSucursalFeriadoDTO> SucursalFeriados { get; set; } = new List<ResponseSucursalFeriadoDTO>();
}