using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Models;

[Table("Usuario")]
[Index("IdDistrito", Name = "IX_Usuario_IdDistrito")]
[Index("IdGenero", Name = "IX_Usuario_IdGenero")]
[Index("IdRol", Name = "IX_Usuario_IdRol")]
public partial class Usuario
{
    [Key]
    public short Id { get; set; }

    [StringLength(50)]
    public string Cedula { get; set; } = null!;

    [StringLength(80)]
    public string Nombre { get; set; } = null!;

    [StringLength(80)]
    public string Apellidos { get; set; } = null!;

    public int Telefono { get; set; }

    [StringLength(150)]
    public string CorreoElectronico { get; set; } = null!;

    public short IdDistrito { get; set; }

    [StringLength(250)]
    public string? DireccionExacta { get; set; }

    public DateOnly FechaNacimiento { get; set; }

    [StringLength(80)]
    [Unicode(false)]
    public string Contrasenna { get; set; } = null!;

    public byte IdGenero { get; set; }

    public bool Activo { get; set; }

    [StringLength(200)]
    public string? UrlFoto { get; set; }

    public byte IdRol { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime FechaCreacion { get; set; }

    [StringLength(70)]
    public string UsuarioCreacion { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? FechaModificacion { get; set; }

    [StringLength(70)]
    public string? UsuarioModificacion { get; set; }

    [ForeignKey("IdDistrito")]
    [InverseProperty("Usuarios")]
    public virtual Distrito IdDistritoNavigation { get; set; } = null!;

    [ForeignKey("IdGenero")]
    [InverseProperty("Usuarios")]
    public virtual Genero IdGeneroNavigation { get; set; } = null!;

    [ForeignKey("IdRol")]
    [InverseProperty("Usuarios")]
    public virtual Rol IdRolNavigation { get; set; } = null!;

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<TokenMaster> TokenMasters { get; set; } = new List<TokenMaster>();

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<UsuarioSucursal> UsuarioSucursals { get; set; } = new List<UsuarioSucursal>();
}
