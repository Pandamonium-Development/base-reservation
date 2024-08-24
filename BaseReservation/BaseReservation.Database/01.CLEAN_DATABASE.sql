USE BaseReservation
GO

BEGIN TRANSACTION ClearDatabase
    WITH MARK N'Cleaning database';

BEGIN TRY
    -- Facturas
    DELETE FROM DetalleFacturaProducto;
    DELETE FROM DetalleFactura;
    DELETE FROM Factura;

    -- Pedido
    DELETE FROM DetallePedidoProducto;
    DELETE FROM DetallePedido;
    DELETE FROM Pedido;

    -- Reserva
    DELETE FROM ReservaPregunta;
    DELETE FROM ReservaServicio;
    DELETE FROM Reserva;

    -- Inventario
    DELETE FROM InventarioProductoMovimiento;
    DELETE FROM InventarioProducto;
    DELETE FROM Inventario;

    -- Proveedor
    DELETE FROM Contacto;
    DELETE FROM Proveedor;

    -- Producto
    DELETE FROM Producto;
    DELETE FROM Categoria;
    DELETE FROM UnidadMedida;

    --Servicio
    DELETE FROM Servicio;
    DELETE FROM TipoServicio;

    -- Cliente
    DELETE FROM Cliente;

    -- Usuario/Seguridad
    DELETE FROM TokenMaster;
    DELETE FROM UsuarioSucursal;
    DELETE FROM Usuario;
    DELETE FROM Rol;

    -- Sucursal
    DELETE FROM SucursalHorarioBloqueo;
    DELETE FROM SucursalHorario;
    DELETE FROM SucursalFeriado;
    DELETE FROM Horario;
    DELETE FROM Feriado;
    DELETE FROM Sucursal;

    -- General
    DELETE FROM TipoPago;
    DELETE FROM Genero;
    DELETE FROM Impuesto;
    DELETE FROM UnidadMedida;

    -- Direcciones
    DELETE FROM Distrito;
    DELETE FROM Canton;
    DELETE FROM Provincia;

    EXEC sp_MSForEachTable '
    IF OBJECTPROPERTY(object_id(''?''), ''TableHasIdentity'') = 1
    DBCC CHECKIDENT (''?'', RESEED, 1)'
    
    COMMIT TRANSACTION ClearDatabase;
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION ClearDatabase;

    SELECT 
        ERROR_NUMBER() AS ErrorNumber,
        ERROR_SEVERITY() AS ErrorSeverity,
        ERROR_STATE() AS ErrorState,
        ERROR_PROCEDURE() AS ErrorProcedure,
        ERROR_LINE() AS ErrorLine,
        ERROR_MESSAGE() AS ErrorMessage;
END CATCH