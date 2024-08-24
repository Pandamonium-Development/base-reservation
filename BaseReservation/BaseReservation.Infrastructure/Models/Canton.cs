using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Models;

[Table("Canton")]
[Index("IdProvincia", Name = "IX_Canton_IdProvincia")]
public partial class Canton
{
    [Key]
    public byte Id { get; set; }

    [StringLength(50)]
    public string Nombre { get; set; } = null!;

    public byte IdProvincia { get; set; }

    [InverseProperty("IdCantonNavigation")]
    public virtual ICollection<Distrito> Distritos { get; set; } = new List<Distrito>();

    [ForeignKey("IdProvincia")]
    [InverseProperty("Cantons")]
    public virtual Provincium IdProvinciaNavigation { get; set; } = null!;
}
