namespace BaseReservation.Application.ResponseDTOs;

public record ResponseTipoServicioDto
{
    public byte Id { get; set; }

    public string Nombre { get; set; } = null!;

    public TimeOnly Duracion { get; set; }

    public virtual ICollection<ResponseServicioDto> Servicios { get; set; } = new List<ResponseServicioDto>();
}