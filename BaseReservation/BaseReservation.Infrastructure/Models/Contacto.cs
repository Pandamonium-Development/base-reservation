using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Models;

[Table("Contacto")]
[Index("IdProveedor", Name = "IX_Contacto_IdProveedor")]
public partial class Contacto : BaseModel
{
    [Key]
    public short Id { get; set; }

    [StringLength(80)]
    public string Nombre { get; set; } = null!;

    [StringLength(80)]
    public string Apellidos { get; set; } = null!;

    public int Telefono { get; set; }

    [StringLength(150)]
    public string CorreoElectronico { get; set; } = null!;

    public byte IdProveedor { get; set; }

    public bool Activo { get; set; }

    [ForeignKey("IdProveedor")]
    [InverseProperty("Contactos")]
    public virtual Proveedor IdProveedorNavigation { get; set; } = null!;
}