using BaseReservation.Application.ResponseDTOs.Enums;

namespace BaseReservation.Application.RequestDTOs;

public record RequestFeriadoDto: RequestBaseDto
{
    public byte Id { get; set; }

    public string Nombre { get; set; } = null!;

    public bool Activo { get; set; }

    public Mes Mes { get; set; }

    public byte Dia { get; set; }
}
