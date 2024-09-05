namespace BaseReservation.Application.ResponseDTOs;

public record ResponseUnidadMedidaDto
{
    public byte Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Simbolo { get; set; } = null!;

    public virtual ICollection<ResponseProductoDto> Productos { get; set; } = new List<ResponseProductoDto>();
}