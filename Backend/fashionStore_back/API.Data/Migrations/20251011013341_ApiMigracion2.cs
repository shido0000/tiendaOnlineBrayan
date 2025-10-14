using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class ApiMigracion2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Ventas");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "VentaDetalles");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Permisos");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "PedidosDetalles");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Monedas");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "ListasDeseosDetalles");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "ListasDeseos");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Inventarios");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Descuentos");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Cupones");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "CategoriasProductos");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "CarritosDetalles");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Carritos");

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(8667), new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(8664) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(8655), new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(8650) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(8688), new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(8686) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(8699), new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(8696) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(8681), new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(8679) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(8674), new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(8672) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(9118), new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(9116) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(9103), new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(9099) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(9136), new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(9134) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(9145), new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(9142) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(9130), new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(9128) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(9124), new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(9122) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336522"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(8370), new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(8319) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336523"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(8401), new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(8398) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336524"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(8408), new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(8406) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("42717fb8-6e3f-4c94-b6b1-a88e8718d0a6"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 10, 21, 33, 41, 477, DateTimeKind.Local).AddTicks(2029), new DateTime(2025, 10, 10, 21, 33, 41, 477, DateTimeKind.Local).AddTicks(1990) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Ventas",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "VentaDetalles",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Usuarios",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Roles",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Productos",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Permisos",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "PedidosDetalles",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Pedidos",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Monedas",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "ListasDeseosDetalles",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "ListasDeseos",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Inventarios",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Descuentos",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Cupones",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "CategoriasProductos",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "CarritosDetalles",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Carritos",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3435), new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3433) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3423), new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3420) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3452), new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3451) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3468), new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3467) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3447), new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3445) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3441), new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3439) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3789), new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3788) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3781), new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3777) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3802), new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3801) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3807), new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3806) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3798), new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3796) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3794), new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3792) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336522"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3256), new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3214) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336523"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3287), new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3286) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336524"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3350), new DateTime(2025, 10, 10, 21, 7, 19, 429, DateTimeKind.Local).AddTicks(3348) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("42717fb8-6e3f-4c94-b6b1-a88e8718d0a6"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 10, 21, 7, 19, 430, DateTimeKind.Local).AddTicks(3419), new DateTime(2025, 10, 10, 21, 7, 19, 430, DateTimeKind.Local).AddTicks(3396) });
        }
    }
}
