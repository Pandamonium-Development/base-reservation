namespace BaseReservation.Application.ResponseDTOs;

public record ResponseUserBranchDto
{
    public short Id { get; set; }

    public short UserId { get; set; }

    public byte BranchId { get; set; }

    public virtual ResponseBranchDto Branch { get; set; } = null!;

    public virtual ResponseUserDto User { get; set; } = null!;
}