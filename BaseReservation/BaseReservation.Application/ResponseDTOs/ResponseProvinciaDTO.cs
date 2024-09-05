namespace BaseReservation.Application.ResponseDTOs;

public record ResponseProvinciaDto
{
    public byte Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<ResponseCantonDto> Cantons { get; set; } = new List<ResponseCantonDto>();
}