using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Models;

[Table("Proveedor")]
[Index("IdDistrito", Name = "IX_Proveedor_IdDistrito")]
public partial class Proveedor
{
    [Key]
    public byte Id { get; set; }

    [StringLength(80)]
    public string Nombre { get; set; } = null!;

    [StringLength(20)]
    public string CedulaJuridica { get; set; } = null!;

    [StringLength(150)]
    public string RasonSocial { get; set; } = null!;

    public int Telefono { get; set; }

    [StringLength(150)]
    public string CorreoElectronico { get; set; } = null!;

    public short IdDistrito { get; set; }

    [StringLength(250)]
    public string? DireccionExacta { get; set; }

    public bool Activo { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime FechaCreacion { get; set; }

    [StringLength(70)]
    public string UsuarioCreacion { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? FechaModificacion { get; set; }

    [StringLength(70)]
    public string? UsuarioModificacion { get; set; }

    [InverseProperty("IdProveedorNavigation")]
    public virtual ICollection<Contacto> Contactos { get; set; } = new List<Contacto>();

    [ForeignKey("IdDistrito")]
    [InverseProperty("Proveedors")]
    public virtual Distrito IdDistritoNavigation { get; set; } = null!;
}
