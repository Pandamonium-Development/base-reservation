using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Models;

[Table("UnidadMedida")]
public partial class UnidadMedida
{
    [Key]
    public byte Id { get; set; }

    [StringLength(25)]
    public string Nombre { get; set; } = null!;

    [StringLength(5)]
    public string Simbolo { get; set; } = null!;

    [InverseProperty("IdUnidadMedidaNavigation")]
    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
