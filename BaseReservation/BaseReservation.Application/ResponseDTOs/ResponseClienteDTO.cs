using BaseReservation.Application.ResponseDTOs.Base;

namespace BaseReservation.Application.ResponseDTOs;

public record ResponseClienteDTO : BaseEntity
{
    public short Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string CorreoElectronico { get; set; } = null!;

    public int Telefono { get; set; }

    public short IdDistrito { get; set; }

    public string? DireccionExacta { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<ResponseFacturaDTO> Facturas { get; set; } = new List<ResponseFacturaDTO>();

    public virtual ICollection<ResponseReservaDTO> Reservas { get; set; } = new List<ResponseReservaDTO>();

    public virtual ResponseDistritoDTO Distrito { get; set; } = null!;
}