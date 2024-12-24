using BaseReservation.Application.ResponseDTOs.Base;

namespace BaseReservation.Application.ResponseDTOs;

public record ResponseBranchDto : BaseEntity
{
    public byte Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Telephone { get; set; }

    public string Email { get; set; } = null!;

    public short DistrictId { get; set; }

    public string? Address { get; set; }

    public bool Active { get; set; }

    public virtual ResponseDistrictDto? District { get; set; } = null!;

    public virtual ICollection<ResponseInventoryDto> Inventories { get; set; } = new List<ResponseInventoryDto>();

    public virtual ICollection<ResponseBranchScheduleDto> BranchSchedules { get; set; } = new List<ResponseBranchScheduleDto>();

    public virtual ICollection<ResponseUserBranchDto> UserBranches { get; set; } = new List<ResponseUserBranchDto>();

    public virtual ICollection<ResponseBranchHolidayDto> BranchHolidays { get; set; } = new List<ResponseBranchHolidayDto>();

    public virtual ICollection<ResponseReservationDto> Reservas { get; set; } = new List<ResponseReservationDto>();

    public virtual ICollection<ResponseOrderDto> Orders { get; set; } = new List<ResponseOrderDto>();

    public virtual ICollection<ResponseInvoiceDto> Invoices { get; set; } = new List<ResponseInvoiceDto>();
}