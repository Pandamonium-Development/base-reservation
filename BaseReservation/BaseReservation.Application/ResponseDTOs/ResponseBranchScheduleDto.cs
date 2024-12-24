namespace BaseReservation.Application.ResponseDTOs;

public record ResponseBranchScheduleDto
{
    public short Id { get; set; }

    public byte BranchId { get; set; }

    public short ScheduleId { get; set; }

    public virtual ResponseScheduleDto Schedule { get; set; } = null!;

    public virtual ResponseBranchDto Branch { get; set; } = null!;

    public virtual ICollection<ResponseBranchScheduleBlockDto> BranchScheduleBlocks { get; set; } = new List<ResponseBranchScheduleBlockDto>();
}