namespace BaseReservation.Application.RequestDTOs;

public record RequestInventoryProductDto : RequestBaseDto
{
    public long Id { get; set; }

    public short InventoryId { get; set; }

    public short ProductId { get; set; }

    public decimal Assignable { get; set; }

    public decimal Mininum { get; set; }

    public decimal Maximum { get; set; }
}