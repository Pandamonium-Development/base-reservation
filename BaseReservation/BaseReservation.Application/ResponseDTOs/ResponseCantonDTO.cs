using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseReservation.Application.ResponseDTOs;

public record ResponseCantonDto
{
    public byte Id { get; set; }

    public string Nombre { get; set; } = null!;

    public byte IdProvincia { get; set; }

    public virtual ICollection<ResponseDistritoDto> Distritos { get; set; } = new List<ResponseDistritoDto>();

    public virtual ResponseProvinciaDto Provincia { get; set; } = null!;
}