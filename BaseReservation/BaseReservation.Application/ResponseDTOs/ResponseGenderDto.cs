namespace BaseReservation.Application.ResponseDTOs;

public record ResponseGenderDto
{
    public byte Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ResponseUserDto> Users { get; set; } = new List<ResponseUserDto>();
}