using BaseReservation.Application.ResponseDTOs.Base;

namespace BaseReservation.Application.ResponseDTOs;

public record ResponseClienteDto : BaseEntity
{
    public short Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string CorreoElectronico { get; set; } = null!;

    public int Telefono { get; set; }

    public short IdDistrito { get; set; }

    public string? DireccionExacta { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<ResponseFacturaDto> Facturas { get; set; } = new List<ResponseFacturaDto>();

    public virtual ICollection<ResponseReservaDto> Reservas { get; set; } = new List<ResponseReservaDto>();

    public virtual ResponseDistritoDto Distrito { get; set; } = null!;
}