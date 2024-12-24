using BaseReservation.Application.ResponseDTOs.Base;

namespace BaseReservation.Application.ResponseDTOs;

public record ResponseReservationDto : BaseEntity
{
    public int Id { get; set; }

    public DateOnly Date { get; set; }

    public TimeOnly Hour { get; set; }

    public byte BranchId { get; set; }

    public short CustomerId { get; set; }

    public string CustomerName { get; set; } = null!;

    public string Status { get; set; } = null!;

    public bool Active { get; set; }

    public virtual ResponseBranchDto Branch { get; set; } = null!;

    public virtual ResponseCustomerDto Customer { get; set; } = null!;

    public virtual ICollection<ResponseReservationQuestionDto> ReservationQuestions { get; set; } = new List<ResponseReservationQuestionDto>();

    public virtual ICollection<ResponseReservationDetailDto> ReservationDetails { get; set; } = new List<ResponseReservationDetailDto>();

    public virtual ICollection<ResponseOrderDto> Orders { get; set; } = new List<ResponseOrderDto>();
}