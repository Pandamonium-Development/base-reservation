namespace BaseReservation.Application.ResponseDTOs;

public record ResponseGeneroDto
{
    public byte Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<ResponseUsuarioDTO> Usuarios { get; set; } = new List<ResponseUsuarioDTO>();
}