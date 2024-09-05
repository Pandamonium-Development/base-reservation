﻿using System.ComponentModel;

namespace BaseReservation.Application.ResponseDTOs;

public record ResponseDetallePedidoProductoDTO
{
    public long Id { get; set; }

    [DisplayName("Detalle")]
    public long IdDetallePedido { get; set; }

    [DisplayName("Producto")]
    public short IdProducto { get; set; }

    public decimal Cantidad { get; set; }

    public virtual ResponseDetallePedidoDTO DetallePedido { get; set; } = null!;

    public virtual ResponseProductoDto Producto { get; set; } = null!;
}