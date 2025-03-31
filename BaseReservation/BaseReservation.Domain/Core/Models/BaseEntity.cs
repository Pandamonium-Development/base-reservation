using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseReservation.Domain.Core.Models;

public class BaseEntity : BaseSimpleDto
{
    [Column(TypeName = "datetime")]
    public DateTime Created { get; set; }

    [StringLength(70)]
    public string CreatedBy { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? Updated { get; set; }

    [StringLength(70)]
    public string? UpdatedBy { get; set; }

    public bool Active { get; set; }
}