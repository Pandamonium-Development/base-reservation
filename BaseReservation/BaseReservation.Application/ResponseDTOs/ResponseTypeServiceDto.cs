namespace BaseReservation.Application.ResponseDTOs;

public record ResponseTypeServiceDto
{
    public byte Id { get; set; }

    public string Name { get; set; } = null!;

    public TimeOnly Duration { get; set; }

    public virtual ICollection<ResponseServiceDto> Services { get; set; } = new List<ResponseServiceDto>();
}