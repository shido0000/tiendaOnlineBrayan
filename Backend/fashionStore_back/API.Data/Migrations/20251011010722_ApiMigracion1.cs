﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class ApiMigracion1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AsientosContables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferenciaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoReferencia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaActualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AsientosContables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoriasProductos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaActualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriasProductos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CuentasContables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EsActivo = table.Column<bool>(type: "bit", nullable: false),
                    EsDeMovimiento = table.Column<bool>(type: "bit", nullable: false),
                    CuentaPadreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaCreado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaActualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuentasContables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CuentasContables_CuentasContables_CuentaPadreId",
                        column: x => x.CuentaPadreId,
                        principalTable: "CuentasContables",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Cupones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Porcentaje = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MontoFijo = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EsActivo = table.Column<bool>(type: "bit", nullable: false),
                    MaximoUsos = table.Column<int>(type: "int", nullable: false),
                    UsosActuales = table.Column<int>(type: "int", nullable: false),
                    MontoMinimoPedido = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaActualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cupones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Monedas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TasaCambio = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    EsActiva = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaActualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monedas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permisos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaActualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permisos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaActualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovimientosContables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AsientoContableId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CuentaContableId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Debe = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Haber = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaActualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimientosContables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovimientosContables_AsientosContables_AsientoContableId",
                        column: x => x.AsientoContableId,
                        principalTable: "AsientosContables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovimientosContables_CuentasContables_CuentaContableId",
                        column: x => x.CuentaContableId,
                        principalTable: "CuentasContables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SKU = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrecioCosto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrecioVenta = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EsActivo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    MonedaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaActualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Productos_Monedas_MonedaId",
                        column: x => x.MonedaId,
                        principalTable: "Monedas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RolPermiso",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RolId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermisoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaActualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolPermiso", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolPermiso_Permisos_PermisoId",
                        column: x => x.PermisoId,
                        principalTable: "Permisos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolPermiso_Roles_RolId",
                        column: x => x.RolId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RolId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Contrasenna = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DebeCambiarContrasenna = table.Column<bool>(type: "bit", nullable: false),
                    EsActivo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaActualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_RolId",
                        column: x => x.RolId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Descuentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Porcentaje = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    MontoFijo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EsActivo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaActualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Descuentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Descuentos_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inventarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CantidadDisponible = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    CantidadReservada = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Ubicacion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaActualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inventarios_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductosCategorias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaActualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductosCategorias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductosCategorias_CategoriasProductos_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "CategoriasProductos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductosCategorias_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carritos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaActualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carritos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carritos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ListasDeseos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaActualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListasDeseos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListasDeseos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonedaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CuponId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaActualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pedidos_Cupones_CuponId",
                        column: x => x.CuponId,
                        principalTable: "Cupones",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pedidos_Monedas_MonedaId",
                        column: x => x.MonedaId,
                        principalTable: "Monedas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pedidos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CarritosDetalles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CarritoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaActualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarritosDetalles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarritosDetalles_Carritos_CarritoId",
                        column: x => x.CarritoId,
                        principalTable: "Carritos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarritosDetalles_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ListasDeseosDetalles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ListaDeseosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaActualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListasDeseosDetalles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListasDeseosDetalles_ListasDeseos_ListaDeseosId",
                        column: x => x.ListaDeseosId,
                        principalTable: "ListasDeseos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListasDeseosDetalles_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PedidosDetalles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PedidoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DescuentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DescuentoAplicado = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    EstadoLinea = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaActualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidosDetalles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PedidosDetalles_Descuentos_DescuentoId",
                        column: x => x.DescuentoId,
                        principalTable: "Descuentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_PedidosDetalles_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidosDetalles_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ventas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Consecutivo = table.Column<int>(type: "int", nullable: false),
                    PedidoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioVendedorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaConfirmacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalFinal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaActualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ventas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ventas_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ventas_Usuarios_UsuarioVendedorId",
                        column: x => x.UsuarioVendedorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ComprobantesVentas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VentaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaEmision = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaActualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComprobantesVentas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComprobantesVentas_Ventas_VentaId",
                        column: x => x.VentaId,
                        principalTable: "Ventas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VentaDetalles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VentaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DescuentoAplicado = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaActualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VentaDetalles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VentaDetalles_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VentaDetalles_Ventas_VentaId",
                        column: x => x.VentaId,
                        principalTable: "Ventas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Permisos",
                columns: new[] { "Id", "ActualizadoPor", "CreadoPor", "Descripcion", "FechaActualizado", "FechaCreado", "Nombre" },
                values: new object[,]
                {
                    { new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"), "", "", "Permite ver, crear, modificar y eliminar usuarios en el sistema.", new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3435), new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3433), "Gestionar usuarios" },
                    { new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"), "", "", "Permite ver los usuarios existentes en el sistema y sus datos.", new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3423), new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3420), "Listar usuarios" },
                    { new Guid("80abf232-a641-478d-8720-f0ae49e8a301"), "", "", "Permite ver los productos existentes en el sistema y sus datos.", new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3452), new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3451), "Listar Productos" },
                    { new Guid("80abf232-a641-478d-8720-f0ae49e8a302"), "", "", "Permite ver, crear, modificar y eliminar productos en el sistema.", new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3468), new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3467), "Gestionar Productos" },
                    { new Guid("90abf232-a641-478d-8720-f0ae49e8a306"), "", "", "Permite ver, crear, modificar y eliminar roles en el sistema.", new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3447), new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3445), "Gestionar rol" },
                    { new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"), "", "", "Permite ver los roles existentes en el sistema y sus datos.", new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3441), new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3439), "Listar roles" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ActualizadoPor", "CreadoPor", "FechaActualizado", "FechaCreado", "Nombre" },
                values: new object[,]
                {
                    { new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336522"), "", "", new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3256), new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3214), "Administrador" },
                    { new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336523"), "", "", new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3287), new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3286), "Vendedor" },
                    { new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336524"), "", "", new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3350), new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3348), "Cliente" }
                });

            migrationBuilder.InsertData(
                table: "RolPermiso",
                columns: new[] { "Id", "ActualizadoPor", "CreadoPor", "FechaActualizado", "FechaCreado", "PermisoId", "RolId" },
                values: new object[,]
                {
                    { new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"), "", "", new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3789), new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3788), new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"), new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336522") },
                    { new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"), "", "", new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3781), new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3777), new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"), new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336522") },
                    { new Guid("80abf232-a641-478d-8720-f0ae49e8a301"), "", "", new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3802), new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3801), new Guid("80abf232-a641-478d-8720-f0ae49e8a301"), new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336522") },
                    { new Guid("80abf232-a641-478d-8720-f0ae49e8a302"), "", "", new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3807), new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3806), new Guid("80abf232-a641-478d-8720-f0ae49e8a302"), new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336522") },
                    { new Guid("90abf232-a641-478d-8720-f0ae49e8a306"), "", "", new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3798), new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3796), new Guid("90abf232-a641-478d-8720-f0ae49e8a306"), new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336522") },
                    { new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"), "", "", new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3794), new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3792), new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"), new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336522") }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "ActualizadoPor", "Apellidos", "Contrasenna", "Correo", "CreadoPor", "DebeCambiarContrasenna", "EsActivo", "FechaActualizado", "FechaCreado", "Nombre", "RolId", "Username" },
                values: new object[] { new Guid("42717fb8-6e3f-4c94-b6b1-a88e8718d0a6"), "", "1", "$2a$10$EixZaYVK1fsbw1Zfbx3OXePaWxn96p36Zf4d0xF4f5f5f5f5f5f5f", "1@api.cu", "", false, true, new DateTime(2025, 10, 10, 21, 7, 19, 430, DateTimeKind.Local).AddTicks(3419), new DateTime(2025, 10, 10, 21, 7, 19, 430, DateTimeKind.Local).AddTicks(3396), "1", new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336522"), "1" });

            migrationBuilder.CreateIndex(
                name: "IX_AsientosContables_Id",
                table: "AsientosContables",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Carritos_Id",
                table: "Carritos",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Carritos_UsuarioId",
                table: "Carritos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_CarritosDetalles_CarritoId",
                table: "CarritosDetalles",
                column: "CarritoId");

            migrationBuilder.CreateIndex(
                name: "IX_CarritosDetalles_Id",
                table: "CarritosDetalles",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CarritosDetalles_ProductoId",
                table: "CarritosDetalles",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoriasProductos_Id",
                table: "CategoriasProductos",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ComprobantesVentas_Id",
                table: "ComprobantesVentas",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ComprobantesVentas_VentaId",
                table: "ComprobantesVentas",
                column: "VentaId");

            migrationBuilder.CreateIndex(
                name: "IX_CuentasContables_CuentaPadreId",
                table: "CuentasContables",
                column: "CuentaPadreId");

            migrationBuilder.CreateIndex(
                name: "IX_CuentasContables_Id",
                table: "CuentasContables",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cupones_Id",
                table: "Cupones",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Descuentos_Id",
                table: "Descuentos",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Descuentos_ProductoId",
                table: "Descuentos",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_Id",
                table: "Inventarios",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_ProductoId",
                table: "Inventarios",
                column: "ProductoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ListasDeseos_Id",
                table: "ListasDeseos",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ListasDeseos_UsuarioId",
                table: "ListasDeseos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ListasDeseosDetalles_Id",
                table: "ListasDeseosDetalles",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ListasDeseosDetalles_ListaDeseosId",
                table: "ListasDeseosDetalles",
                column: "ListaDeseosId");

            migrationBuilder.CreateIndex(
                name: "IX_ListasDeseosDetalles_ProductoId",
                table: "ListasDeseosDetalles",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Monedas_Codigo",
                table: "Monedas",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Monedas_Descripcion",
                table: "Monedas",
                column: "Descripcion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Monedas_Id",
                table: "Monedas",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosContables_AsientoContableId",
                table: "MovimientosContables",
                column: "AsientoContableId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosContables_CuentaContableId",
                table: "MovimientosContables",
                column: "CuentaContableId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosContables_Id",
                table: "MovimientosContables",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_CuponId",
                table: "Pedidos",
                column: "CuponId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_Id",
                table: "Pedidos",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_MonedaId",
                table: "Pedidos",
                column: "MonedaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_UsuarioId",
                table: "Pedidos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidosDetalles_DescuentoId",
                table: "PedidosDetalles",
                column: "DescuentoId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidosDetalles_Id",
                table: "PedidosDetalles",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PedidosDetalles_PedidoId",
                table: "PedidosDetalles",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidosDetalles_ProductoId",
                table: "PedidosDetalles",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Permisos_Id",
                table: "Permisos",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Productos_Codigo",
                table: "Productos",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Productos_Id",
                table: "Productos",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Productos_MonedaId",
                table: "Productos",
                column: "MonedaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductosCategorias_CategoriaId",
                table: "ProductosCategorias",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductosCategorias_Id",
                table: "ProductosCategorias",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductosCategorias_ProductoId",
                table: "ProductosCategorias",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Id",
                table: "Roles",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RolPermiso_Id",
                table: "RolPermiso",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RolPermiso_PermisoId",
                table: "RolPermiso",
                column: "PermisoId");

            migrationBuilder.CreateIndex(
                name: "IX_RolPermiso_RolId_PermisoId",
                table: "RolPermiso",
                columns: new[] { "RolId", "PermisoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Correo",
                table: "Usuarios",
                column: "Correo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Id",
                table: "Usuarios",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Nombre_Apellidos",
                table: "Usuarios",
                columns: new[] { "Nombre", "Apellidos" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_RolId",
                table: "Usuarios",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Username",
                table: "Usuarios",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VentaDetalles_Id",
                table: "VentaDetalles",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VentaDetalles_ProductoId",
                table: "VentaDetalles",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_VentaDetalles_VentaId",
                table: "VentaDetalles",
                column: "VentaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_Id",
                table: "Ventas",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_PedidoId",
                table: "Ventas",
                column: "PedidoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_UsuarioVendedorId",
                table: "Ventas",
                column: "UsuarioVendedorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarritosDetalles");

            migrationBuilder.DropTable(
                name: "ComprobantesVentas");

            migrationBuilder.DropTable(
                name: "Inventarios");

            migrationBuilder.DropTable(
                name: "ListasDeseosDetalles");

            migrationBuilder.DropTable(
                name: "MovimientosContables");

            migrationBuilder.DropTable(
                name: "PedidosDetalles");

            migrationBuilder.DropTable(
                name: "ProductosCategorias");

            migrationBuilder.DropTable(
                name: "RolPermiso");

            migrationBuilder.DropTable(
                name: "VentaDetalles");

            migrationBuilder.DropTable(
                name: "Carritos");

            migrationBuilder.DropTable(
                name: "ListasDeseos");

            migrationBuilder.DropTable(
                name: "AsientosContables");

            migrationBuilder.DropTable(
                name: "CuentasContables");

            migrationBuilder.DropTable(
                name: "Descuentos");

            migrationBuilder.DropTable(
                name: "CategoriasProductos");

            migrationBuilder.DropTable(
                name: "Permisos");

            migrationBuilder.DropTable(
                name: "Ventas");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Cupones");

            migrationBuilder.DropTable(
                name: "Monedas");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
