using BaseReservation.Application.ResponseDTOs.Base;

namespace BaseReservation.Application.ResponseDTOs;

public record ResponseCategoriaDto : BaseEntity
{
    public byte Id { get; set; }

    public string Codigo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public virtual ICollection<ResponseProductoDTO> Productos { get; set; } = new List<ResponseProductoDTO>();
}