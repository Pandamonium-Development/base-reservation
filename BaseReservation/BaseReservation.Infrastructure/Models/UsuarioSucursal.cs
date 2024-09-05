using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Models;

[Table("UsuarioSucursal")]
[Index("IdSucursal", Name = "IX_UsuarioSucursal_IdSucursal")]
[Index("IdUsuario", Name = "IX_UsuarioSucursal_IdUsuario")]
public partial class UsuarioSucursal
{
    [Key]
    public short Id { get; set; }

    public short IdUsuario { get; set; }

    public byte IdSucursal { get; set; }

    [ForeignKey("IdSucursal")]
    [InverseProperty("UsuarioSucursals")]
    public virtual Sucursal IdSucursalNavigation { get; set; } = null!;

    [ForeignKey("IdUsuario")]
    [InverseProperty("UsuarioSucursals")]
    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}