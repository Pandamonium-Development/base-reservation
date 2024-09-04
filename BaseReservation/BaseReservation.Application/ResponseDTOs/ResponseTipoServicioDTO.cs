namespace BaseReservation.Application.ResponseDTOs;

public record ResponseTipoServicioDTO
{
    public byte Id { get; set; }

    public string Nombre { get; set; } = null!;

    public TimeOnly Duracion { get; set; }

    public virtual ICollection<ResponseServicioDTO> Servicios { get; set; } = new List<ResponseServicioDTO>();
}