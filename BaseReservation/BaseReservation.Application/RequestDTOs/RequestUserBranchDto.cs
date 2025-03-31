namespace BaseReservation.Application.RequestDTOs;

public record RequestUserBranchDto : RequestBaseDto
{
    public long UserId { get; set; }

    public long BranchId { get; set; }
}