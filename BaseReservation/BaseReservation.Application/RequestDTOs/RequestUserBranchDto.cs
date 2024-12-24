namespace BaseReservation.Application.RequestDTOs;

public record RequestUserBranchDto
{
    public short Id { get; set; }

    public short UserId { get; set; }

    public byte BranchId { get; set; }
}