using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Models;

[Table("SucursalFeriado")]
[Index("IdFeriado", Name = "IX_SucursalFeriado_IdFeriado")]
[Index("IdSucursal", Name = "IX_SucursalFeriado_IdSucursal")]
public partial class SucursalFeriado
{
    [Key]
    public short Id { get; set; }

    public byte IdFeriado { get; set; }

    public byte IdSucursal { get; set; }

    public short Anno { get; set; }

    public DateOnly Fecha { get; set; }

    [ForeignKey("IdFeriado")]
    [InverseProperty("SucursalFeriados")]
    public virtual Feriado IdFeriadoNavigation { get; set; } = null!;

    [ForeignKey("IdSucursal")]
    [InverseProperty("SucursalFeriados")]
    public virtual Sucursal IdSucursalNavigation { get; set; } = null!;
}
