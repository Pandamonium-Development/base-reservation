using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseReservation.Application.ResponseDTOs;

public record ResponseCantonDTO
{
    public byte Id { get; set; }

    public string Nombre { get; set; } = null!;

    public byte IdProvincia { get; set; }

    public virtual ICollection<ResponseDistritoDTO> Distritos { get; set; } = new List<ResponseDistritoDTO>();

    public virtual ResponseProvinciaDTO Provincia { get; set; } = null!;
}