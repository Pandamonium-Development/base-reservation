using BaseReservation.Application.ResponseDTOs.Base;

namespace BaseReservation.Application.ResponseDTOs;

public record ResponseProveedorDto : BaseEntity
{
    public byte Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string CedulaJuridica { get; set; } = null!;

    public string RasonSocial { get; set; } = null!;

    public int Telefono { get; set; }

    public string CorreoElectronico { get; set; } = null!;

    public short IdDistrito { get; set; }

    public string? DireccionExacta { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<ResponseContactoDTO> Contactos { get; set; } = new List<ResponseContactoDTO>();

    public virtual ResponseDistritoDTO Distrito { get; set; } = null!;
}