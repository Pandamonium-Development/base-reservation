namespace BaseReservation.Application.ResponseDTOs;

public record ResponseUnidadMedidaDTO
{
    public byte Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Simbolo { get; set; } = null!;

    public virtual ICollection<ResponseProductoDTO> Productos { get; set; } = new List<ResponseProductoDTO>();
}