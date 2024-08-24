﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Models;

[Table("Genero")]
public partial class Genero
{
    [Key]
    public byte Id { get; set; }

    [StringLength(25)]
    public string Nombre { get; set; } = null!;

    [InverseProperty("IdGeneroNavigation")]
    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
