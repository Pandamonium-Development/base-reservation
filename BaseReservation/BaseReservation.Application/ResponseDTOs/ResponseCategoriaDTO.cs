﻿using BaseReservation.Application.ResponseDTOs.Base;

namespace BaseReservation.Application.ResponseDTOs;

public record ResponseCategoriaDTO : BaseEntity
{
    public byte Id { get; set; }

    public string Codigo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public virtual ICollection<ResponseProductoDto> Productos { get; set; } = new List<ResponseProductoDto>();
}