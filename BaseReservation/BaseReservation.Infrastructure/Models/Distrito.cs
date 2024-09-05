using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Models;

[Table("Distrito")]
[Index("IdCanton", Name = "IX_Distrito_IdCanton")]
public partial class Distrito
{
    [Key]
    public short Id { get; set; }

    [StringLength(50)]
    public string Nombre { get; set; } = null!;

    public byte IdCanton { get; set; }

    [InverseProperty("IdDistritoNavigation")]
    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    [ForeignKey("IdCanton")]
    [InverseProperty("Distritos")]
    public virtual Canton IdCantonNavigation { get; set; } = null!;

    [InverseProperty("IdDistritoNavigation")]
    public virtual ICollection<Proveedor> Proveedors { get; set; } = new List<Proveedor>();

    [InverseProperty("IdDistritoNavigation")]
    public virtual ICollection<Sucursal> Sucursals { get; set; } = new List<Sucursal>();

    [InverseProperty("IdDistritoNavigation")]
    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}