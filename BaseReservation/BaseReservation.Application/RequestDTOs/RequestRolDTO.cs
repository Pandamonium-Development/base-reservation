namespace BaseReservation.Application.RequestDTOs;

public record RequestRolDto: RequestBaseDto
{
    public byte Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public string Tipo { get; set; } = null!;

    public bool Activo { get; set; }
}