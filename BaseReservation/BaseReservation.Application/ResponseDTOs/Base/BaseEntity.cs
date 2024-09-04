namespace BaseReservation.Application.ResponseDTOs.Base;

public record BaseEntity
{
    public DateTime FechaCreacion { get; set; }

    public string UsuarioCreacion { get; set; } = null!;

    public DateTime? FechaModificacion { get; set; }
    
    public string? UsuarioModificacion { get; set; }
}
