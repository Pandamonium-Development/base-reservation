using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseReservation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsuarioModificacion = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstadoPedido",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoPedido", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Feriado",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Mes = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Dia = table.Column<byte>(type: "tinyint", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsuarioModificacion = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feriado", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genero",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genero", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Horario",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HoraInicio = table.Column<TimeOnly>(type: "time", nullable: false),
                    HoraFin = table.Column<TimeOnly>(type: "time", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsuarioModificacion = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Horario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Impuesto",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Porcentaje = table.Column<decimal>(type: "decimal(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Impuesto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Provincia",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provincia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsuarioModificacion = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoPago",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Referencia = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoPago", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoServicio",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Duracion = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoServicio", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnidadMedida",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Simbolo = table.Column<string>(type: "nchar(5)", fixedLength: true, maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadMedida", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Canton",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IdProvincia = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Canton", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Canton_Provincia",
                        column: x => x.IdProvincia,
                        principalTable: "Provincia",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Servicio",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IdTipoServicio = table.Column<byte>(type: "tinyint", nullable: false),
                    Tarifa = table.Column<decimal>(type: "money", nullable: false),
                    Observacion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsuarioModificacion = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Servicio_TipoServicio",
                        column: x => x.IdTipoServicio,
                        principalTable: "TipoServicio",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IdCategoria = table.Column<byte>(type: "tinyint", nullable: false),
                    Costo = table.Column<decimal>(type: "money", nullable: false),
                    SKU = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IdUnidadMedida = table.Column<byte>(type: "tinyint", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsuarioModificacion = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Producto_Categoria",
                        column: x => x.IdCategoria,
                        principalTable: "Categoria",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Producto_UnidadMedida",
                        column: x => x.IdUnidadMedida,
                        principalTable: "UnidadMedida",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Distrito",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IdCanton = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Distrito", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Distrito_Canton",
                        column: x => x.IdCanton,
                        principalTable: "Canton",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    CorreoElectronico = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Telefono = table.Column<int>(type: "int", nullable: false),
                    IdDistrito = table.Column<short>(type: "smallint", nullable: false),
                    DireccionExacta = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false, defaultValue: ""),
                    FechaModificacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsuarioModificacion = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cliente_Distrito",
                        column: x => x.IdDistrito,
                        principalTable: "Distrito",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Proveedor",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    CedulaJuridica = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    RasonSocial = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Telefono = table.Column<int>(type: "int", nullable: false),
                    CorreoElectronico = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IdDistrito = table.Column<short>(type: "smallint", nullable: false),
                    DireccionExacta = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsuarioModificacion = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proveedor_Distrito",
                        column: x => x.IdDistrito,
                        principalTable: "Distrito",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Sucursal",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Telefono = table.Column<int>(type: "int", nullable: false),
                    CorreoElectronico = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IdDistrito = table.Column<short>(type: "smallint", nullable: false),
                    DireccionExacta = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsuarioModificacion = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sucursal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sucursal_Distrito",
                        column: x => x.IdDistrito,
                        principalTable: "Distrito",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cedula = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Telefono = table.Column<int>(type: "int", nullable: false),
                    CorreoElectronico = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IdDistrito = table.Column<short>(type: "smallint", nullable: false),
                    DireccionExacta = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    FechaNacimiento = table.Column<DateOnly>(type: "date", nullable: false),
                    Contrasenna = table.Column<string>(type: "varchar(80)", unicode: false, maxLength: 80, nullable: false),
                    IdGenero = table.Column<byte>(type: "tinyint", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    UrlFoto = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IdRol = table.Column<byte>(type: "tinyint", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsuarioModificacion = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_Distrito",
                        column: x => x.IdDistrito,
                        principalTable: "Distrito",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Usuario_Genero",
                        column: x => x.IdGenero,
                        principalTable: "Genero",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Usuario_Rol",
                        column: x => x.IdRol,
                        principalTable: "Rol",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Contacto",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Telefono = table.Column<int>(type: "int", nullable: false),
                    CorreoElectronico = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IdProveedor = table.Column<byte>(type: "tinyint", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsuarioModificacion = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacto_Proveedor",
                        column: x => x.IdProveedor,
                        principalTable: "Proveedor",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Inventario",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSucursal = table.Column<byte>(type: "tinyint", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    TipoInventario = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsuarioModificacion = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inventario_Sucursal",
                        column: x => x.IdSucursal,
                        principalTable: "Sucursal",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Reserva",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSucursal = table.Column<byte>(type: "tinyint", nullable: false),
                    IdCliente = table.Column<short>(type: "smallint", nullable: false),
                    NombreCliente = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Fecha = table.Column<DateOnly>(type: "date", nullable: false),
                    Hora = table.Column<TimeOnly>(type: "time", nullable: false),
                    Estado = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false, defaultValue: "P"),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsuarioModificacion = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reserva", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reserva_Cliente",
                        column: x => x.IdCliente,
                        principalTable: "Cliente",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reserva_Sucursal",
                        column: x => x.IdSucursal,
                        principalTable: "Sucursal",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SucursalFeriado",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdFeriado = table.Column<byte>(type: "tinyint", nullable: false),
                    IdSucursal = table.Column<byte>(type: "tinyint", nullable: false),
                    Anno = table.Column<short>(type: "smallint", nullable: false),
                    Fecha = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SucursalFeriado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SucursalFeriado_Feriado",
                        column: x => x.IdFeriado,
                        principalTable: "Feriado",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SucursalFeriado_Sucursal",
                        column: x => x.IdSucursal,
                        principalTable: "Sucursal",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SucursalHorario",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSucursal = table.Column<byte>(type: "tinyint", nullable: false),
                    IdHorario = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SucursalHorario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SucursalHorario_Horario",
                        column: x => x.IdHorario,
                        principalTable: "Horario",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SucursalHorario_Sucursal",
                        column: x => x.IdSucursal,
                        principalTable: "Sucursal",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TokenMaster",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    JwtId = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    FechaVencimiento = table.Column<DateTime>(type: "datetime", nullable: false),
                    Utilizado = table.Column<bool>(type: "bit", nullable: false),
                    IdUsuario = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TokenMaster", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TokenMaster_Usuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UsuarioSucursal",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<short>(type: "smallint", nullable: false),
                    IdSucursal = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioSucursal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioSucursal_Sucursal",
                        column: x => x.IdSucursal,
                        principalTable: "Sucursal",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UsuarioSucursal_Usuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InventarioProducto",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdInventario = table.Column<short>(type: "smallint", nullable: false),
                    IdProducto = table.Column<short>(type: "smallint", nullable: false),
                    Disponible = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Minima = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Maxima = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioModificacion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventarioProducto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventarioProducto_Inventario",
                        column: x => x.IdInventario,
                        principalTable: "Inventario",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InventarioProducto_Producto",
                        column: x => x.IdProducto,
                        principalTable: "Producto",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DetalleReserva",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdReserva = table.Column<int>(type: "int", nullable: false),
                    IdServicio = table.Column<byte>(type: "tinyint", nullable: true),
                    IdProducto = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleReserva", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetalleReserva_Producto",
                        column: x => x.IdProducto,
                        principalTable: "Producto",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DetalleReserva_Reserva",
                        column: x => x.IdReserva,
                        principalTable: "Reserva",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DetalleReserva_Servicio",
                        column: x => x.IdServicio,
                        principalTable: "Servicio",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Pedido",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSucursal = table.Column<byte>(type: "tinyint", nullable: false),
                    IdReserva = table.Column<int>(type: "int", nullable: false),
                    IdCliente = table.Column<short>(type: "smallint", nullable: false),
                    NombreCliente = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: false),
                    Fecha = table.Column<DateOnly>(type: "date", nullable: false),
                    IdTipoPago = table.Column<byte>(type: "tinyint", nullable: false),
                    Consecutivo = table.Column<short>(type: "smallint", nullable: false),
                    IdImpuesto = table.Column<byte>(type: "tinyint", nullable: false),
                    PorcentajeImpuesto = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    SubTotal = table.Column<decimal>(type: "money", nullable: false),
                    MontoImpuesto = table.Column<decimal>(type: "money", nullable: false),
                    MontoTotal = table.Column<decimal>(type: "money", nullable: false),
                    IdEstadoPedido = table.Column<byte>(type: "tinyint", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsuarioModificacion = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pedido_Cliente",
                        column: x => x.IdCliente,
                        principalTable: "Cliente",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pedido_EstadoPedido",
                        column: x => x.IdEstadoPedido,
                        principalTable: "EstadoPedido",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pedido_Impuesto",
                        column: x => x.IdImpuesto,
                        principalTable: "Impuesto",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pedido_Reserva",
                        column: x => x.IdReserva,
                        principalTable: "Reserva",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pedido_Sucursal",
                        column: x => x.IdSucursal,
                        principalTable: "Sucursal",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pedido_TipoPago",
                        column: x => x.IdTipoPago,
                        principalTable: "TipoPago",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReservaPregunta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdReserva = table.Column<int>(type: "int", nullable: false),
                    Pregunta = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Respuesta = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsuarioModificacion = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservaPregunta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservaPregunta_Reserva",
                        column: x => x.IdReserva,
                        principalTable: "Reserva",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SucursalHorarioBloqueo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSucursalHorario = table.Column<short>(type: "smallint", nullable: false),
                    HoraInicio = table.Column<TimeOnly>(type: "time", nullable: false),
                    HoraFin = table.Column<TimeOnly>(type: "time", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SucursalHorarioBloqueo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SucursalHorarioBloqueo_SucursalHorario",
                        column: x => x.IdSucursalHorario,
                        principalTable: "SucursalHorario",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InventarioProductoMovimiento",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdInventarioProducto = table.Column<long>(type: "bigint", nullable: false),
                    TipoMovimiento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cantidad = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsuarioModificacion = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventarioProductoMovimiento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventarioProductoMovimiento_InventarioProducto",
                        column: x => x.IdInventarioProducto,
                        principalTable: "InventarioProducto",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DetallePedido",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPedido = table.Column<long>(type: "bigint", nullable: false),
                    IdServicio = table.Column<byte>(type: "tinyint", nullable: true),
                    IdProducto = table.Column<short>(type: "smallint", nullable: true),
                    NumeroLinea = table.Column<byte>(type: "tinyint", nullable: false),
                    Cantidad = table.Column<short>(type: "smallint", nullable: false),
                    TarifaServicio = table.Column<decimal>(type: "money", nullable: false),
                    MontoSubtotal = table.Column<decimal>(type: "money", nullable: false),
                    MontoImpuesto = table.Column<decimal>(type: "money", nullable: false),
                    MontoTotal = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallePedido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetallePedido_Pedido",
                        column: x => x.IdPedido,
                        principalTable: "Pedido",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DetallePedido_Producto",
                        column: x => x.IdProducto,
                        principalTable: "Producto",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DetallePedido_Servicio",
                        column: x => x.IdServicio,
                        principalTable: "Servicio",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Factura",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSucursal = table.Column<byte>(type: "tinyint", nullable: false),
                    IdPedido = table.Column<long>(type: "bigint", nullable: true),
                    IdCliente = table.Column<short>(type: "smallint", nullable: false),
                    NombreCliente = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: false),
                    Fecha = table.Column<DateOnly>(type: "date", nullable: false),
                    IdTipoPago = table.Column<byte>(type: "tinyint", nullable: false),
                    Consecutivo = table.Column<short>(type: "smallint", nullable: false),
                    IdImpuesto = table.Column<byte>(type: "tinyint", nullable: false),
                    PorcentajeImpuesto = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    SubTotal = table.Column<decimal>(type: "money", nullable: false),
                    MontoImpuesto = table.Column<decimal>(type: "money", nullable: false),
                    MontoTotal = table.Column<decimal>(type: "money", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsuarioModificacion = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factura", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Factura_Cliente",
                        column: x => x.IdCliente,
                        principalTable: "Cliente",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Factura_Impuesto",
                        column: x => x.IdImpuesto,
                        principalTable: "Impuesto",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Factura_Pedido",
                        column: x => x.IdPedido,
                        principalTable: "Pedido",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Factura_Sucursal",
                        column: x => x.IdSucursal,
                        principalTable: "Sucursal",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Factura_TipoPago",
                        column: x => x.IdTipoPago,
                        principalTable: "TipoPago",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DetallePedidoProducto",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDetallePedido = table.Column<long>(type: "bigint", nullable: false),
                    IdProducto = table.Column<short>(type: "smallint", nullable: false),
                    Cantidad = table.Column<decimal>(type: "decimal(6,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallePedidoProducto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetallePedidoProducto_DetallePedido",
                        column: x => x.IdDetallePedido,
                        principalTable: "DetallePedido",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DetallePedidoProducto_Producto",
                        column: x => x.IdProducto,
                        principalTable: "Producto",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DetalleFactura",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdFactura = table.Column<long>(type: "bigint", nullable: false),
                    IdServicio = table.Column<byte>(type: "tinyint", nullable: true),
                    IdProducto = table.Column<short>(type: "smallint", nullable: true),
                    NumeroLinea = table.Column<byte>(type: "tinyint", nullable: false),
                    Cantidad = table.Column<short>(type: "smallint", nullable: false),
                    TarifaServicio = table.Column<decimal>(type: "money", nullable: false),
                    MontoSubtotal = table.Column<decimal>(type: "money", nullable: false),
                    MontoImpuesto = table.Column<decimal>(type: "money", nullable: false),
                    MontoTotal = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleFactura", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetalleFactura_Factura",
                        column: x => x.IdFactura,
                        principalTable: "Factura",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DetalleFactura_Producto",
                        column: x => x.IdProducto,
                        principalTable: "Producto",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DetalleFactura_Servicio",
                        column: x => x.IdServicio,
                        principalTable: "Servicio",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DetalleFacturaProducto",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDetalleFactura = table.Column<long>(type: "bigint", nullable: false),
                    IdProducto = table.Column<short>(type: "smallint", nullable: false),
                    Cantidad = table.Column<decimal>(type: "decimal(6,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleFacturaProducto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetalleFacturaProducto_DetalleFactura",
                        column: x => x.IdDetalleFactura,
                        principalTable: "DetalleFactura",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DetalleFacturaProducto_Producto",
                        column: x => x.IdProducto,
                        principalTable: "Producto",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Canton_IdProvincia",
                table: "Canton",
                column: "IdProvincia");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_IdDistrito",
                table: "Cliente",
                column: "IdDistrito");

            migrationBuilder.CreateIndex(
                name: "IX_Contacto_IdProveedor",
                table: "Contacto",
                column: "IdProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleFactura_IdFactura",
                table: "DetalleFactura",
                column: "IdFactura");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleFactura_IdProducto",
                table: "DetalleFactura",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleFactura_IdServicio",
                table: "DetalleFactura",
                column: "IdServicio");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleFacturaProducto_IdDetalleFactura",
                table: "DetalleFacturaProducto",
                column: "IdDetalleFactura");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleFacturaProducto_IdProducto",
                table: "DetalleFacturaProducto",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_DetallePedido_IdPedido",
                table: "DetallePedido",
                column: "IdPedido");

            migrationBuilder.CreateIndex(
                name: "IX_DetallePedido_IdProducto",
                table: "DetallePedido",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_DetallePedido_IdServicio",
                table: "DetallePedido",
                column: "IdServicio");

            migrationBuilder.CreateIndex(
                name: "IX_DetallePedidoProducto_IdDetallePedido",
                table: "DetallePedidoProducto",
                column: "IdDetallePedido");

            migrationBuilder.CreateIndex(
                name: "IX_DetallePedidoProducto_IdProducto",
                table: "DetallePedidoProducto",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleReserva_IdProducto",
                table: "DetalleReserva",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_ReservaServicio_IdReserva",
                table: "DetalleReserva",
                column: "IdReserva");

            migrationBuilder.CreateIndex(
                name: "IX_ReservaServicio_IdServicio",
                table: "DetalleReserva",
                column: "IdServicio");

            migrationBuilder.CreateIndex(
                name: "IX_Distrito_IdCanton",
                table: "Distrito",
                column: "IdCanton");

            migrationBuilder.CreateIndex(
                name: "IX_Factura_IdCliente",
                table: "Factura",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Factura_IdImpuesto",
                table: "Factura",
                column: "IdImpuesto");

            migrationBuilder.CreateIndex(
                name: "IX_Factura_IdPedido",
                table: "Factura",
                column: "IdPedido");

            migrationBuilder.CreateIndex(
                name: "IX_Factura_IdSucursal",
                table: "Factura",
                column: "IdSucursal");

            migrationBuilder.CreateIndex(
                name: "IX_Factura_IdTipoPago",
                table: "Factura",
                column: "IdTipoPago");

            migrationBuilder.CreateIndex(
                name: "IX_Inventario_IdSucursal",
                table: "Inventario",
                column: "IdSucursal");

            migrationBuilder.CreateIndex(
                name: "IX_InventarioProducto_IdInventario",
                table: "InventarioProducto",
                column: "IdInventario");

            migrationBuilder.CreateIndex(
                name: "IX_InventarioProducto_IdProducto",
                table: "InventarioProducto",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_InventarioProductoMovimiento_IdInventarioProducto",
                table: "InventarioProductoMovimiento",
                column: "IdInventarioProducto");

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_IdCliente",
                table: "Pedido",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_IdEstadoPedido",
                table: "Pedido",
                column: "IdEstadoPedido");

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_IdImpuesto",
                table: "Pedido",
                column: "IdImpuesto");

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_IdReserva",
                table: "Pedido",
                column: "IdReserva");

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_IdSucursal",
                table: "Pedido",
                column: "IdSucursal");

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_IdTipoPago",
                table: "Pedido",
                column: "IdTipoPago");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_IdCategoria",
                table: "Producto",
                column: "IdCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_IdUnidadMedida",
                table: "Producto",
                column: "IdUnidadMedida");

            migrationBuilder.CreateIndex(
                name: "IX_Proveedor_IdDistrito",
                table: "Proveedor",
                column: "IdDistrito");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_IdCliente",
                table: "Reserva",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_IdSucursal",
                table: "Reserva",
                column: "IdSucursal");

            migrationBuilder.CreateIndex(
                name: "IX_ReservaPregunta_IdReserva",
                table: "ReservaPregunta",
                column: "IdReserva");

            migrationBuilder.CreateIndex(
                name: "IX_Servicio_IdTipoServicio",
                table: "Servicio",
                column: "IdTipoServicio");

            migrationBuilder.CreateIndex(
                name: "IX_Sucursal_IdDistrito",
                table: "Sucursal",
                column: "IdDistrito");

            migrationBuilder.CreateIndex(
                name: "IX_SucursalFeriado_IdFeriado",
                table: "SucursalFeriado",
                column: "IdFeriado");

            migrationBuilder.CreateIndex(
                name: "IX_SucursalFeriado_IdSucursal",
                table: "SucursalFeriado",
                column: "IdSucursal");

            migrationBuilder.CreateIndex(
                name: "IX_SucursalHorario_IdHorario",
                table: "SucursalHorario",
                column: "IdHorario");

            migrationBuilder.CreateIndex(
                name: "IX_SucursalHorario_IdSucursal",
                table: "SucursalHorario",
                column: "IdSucursal");

            migrationBuilder.CreateIndex(
                name: "IX_SucursalHorarioBloqueo_IdSucursalHorario",
                table: "SucursalHorarioBloqueo",
                column: "IdSucursalHorario");

            migrationBuilder.CreateIndex(
                name: "IX_TokenMaster_IdUsuario",
                table: "TokenMaster",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_IdDistrito",
                table: "Usuario",
                column: "IdDistrito");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_IdGenero",
                table: "Usuario",
                column: "IdGenero");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_IdRol",
                table: "Usuario",
                column: "IdRol");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioSucursal_IdSucursal",
                table: "UsuarioSucursal",
                column: "IdSucursal");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioSucursal_IdUsuario",
                table: "UsuarioSucursal",
                column: "IdUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacto");

            migrationBuilder.DropTable(
                name: "DetalleFacturaProducto");

            migrationBuilder.DropTable(
                name: "DetallePedidoProducto");

            migrationBuilder.DropTable(
                name: "DetalleReserva");

            migrationBuilder.DropTable(
                name: "InventarioProductoMovimiento");

            migrationBuilder.DropTable(
                name: "ReservaPregunta");

            migrationBuilder.DropTable(
                name: "SucursalFeriado");

            migrationBuilder.DropTable(
                name: "SucursalHorarioBloqueo");

            migrationBuilder.DropTable(
                name: "TokenMaster");

            migrationBuilder.DropTable(
                name: "UsuarioSucursal");

            migrationBuilder.DropTable(
                name: "Proveedor");

            migrationBuilder.DropTable(
                name: "DetalleFactura");

            migrationBuilder.DropTable(
                name: "DetallePedido");

            migrationBuilder.DropTable(
                name: "InventarioProducto");

            migrationBuilder.DropTable(
                name: "Feriado");

            migrationBuilder.DropTable(
                name: "SucursalHorario");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Factura");

            migrationBuilder.DropTable(
                name: "Servicio");

            migrationBuilder.DropTable(
                name: "Inventario");

            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropTable(
                name: "Horario");

            migrationBuilder.DropTable(
                name: "Genero");

            migrationBuilder.DropTable(
                name: "Rol");

            migrationBuilder.DropTable(
                name: "Pedido");

            migrationBuilder.DropTable(
                name: "TipoServicio");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "UnidadMedida");

            migrationBuilder.DropTable(
                name: "EstadoPedido");

            migrationBuilder.DropTable(
                name: "Impuesto");

            migrationBuilder.DropTable(
                name: "Reserva");

            migrationBuilder.DropTable(
                name: "TipoPago");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Sucursal");

            migrationBuilder.DropTable(
                name: "Distrito");

            migrationBuilder.DropTable(
                name: "Canton");

            migrationBuilder.DropTable(
                name: "Provincia");
        }
    }
}
