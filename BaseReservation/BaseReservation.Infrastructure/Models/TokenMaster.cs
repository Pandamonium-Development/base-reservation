using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Models;

[Table("TokenMaster")]
[Index("IdUsuario", Name = "IX_TokenMaster_IdUsuario")]
public partial class TokenMaster
{
    [Key]
    public long Id { get; set; }

    [StringLength(250)]
    public string Token { get; set; } = null!;

    [StringLength(250)]
    public string JwtId { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime FechaCreacion { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime FechaVencimiento { get; set; }

    public bool Utilizado { get; set; }

    public short IdUsuario { get; set; }

    [ForeignKey("IdUsuario")]
    [InverseProperty("TokenMasters")]
    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
