namespace BaseReservation.Application.ResponseDTOs.Base;

public record BaseEntity
{
    public DateTime Created { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? Updated { get; set; }

    public string? UpdatedBy { get; set; }
}
