namespace BaseReservation.Application.ResponseDTOs;

public record ResponseGeneroDto
{
    public byte Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<ResponseUsuarioDto> Usuarios { get; set; } = new List<ResponseUsuarioDto>();
}