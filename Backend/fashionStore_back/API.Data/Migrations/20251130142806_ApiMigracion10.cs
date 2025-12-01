using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class ApiMigracion10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VentaDetalles_Productos_ProductoId",
                table: "VentaDetalles");

            migrationBuilder.RenameColumn(
                name: "ProductoId",
                table: "VentaDetalles",
                newName: "ProductoVarianteId");

            migrationBuilder.RenameIndex(
                name: "IX_VentaDetalles_ProductoId",
                table: "VentaDetalles",
                newName: "IX_VentaDetalles_ProductoVarianteId");

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3199), new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3197) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3190), new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3188) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3215), new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3214) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3224), new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3222) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3210), new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3208) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3204), new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3202) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3498), new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3497) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3486), new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3483) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3513), new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3511) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3518), new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3517) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3509), new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3507) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3503), new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3502) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336522"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3077), new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3028) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336523"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3102), new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3100) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336524"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3107), new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3106) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("42717fb8-6e3f-4c94-b6b1-a88e8718d0a6"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 30, 9, 28, 6, 157, DateTimeKind.Local).AddTicks(2953), new DateTime(2025, 11, 30, 9, 28, 6, 157, DateTimeKind.Local).AddTicks(2936) });

            migrationBuilder.AddForeignKey(
                name: "FK_VentaDetalles_ProductoVariantes_ProductoVarianteId",
                table: "VentaDetalles",
                column: "ProductoVarianteId",
                principalTable: "ProductoVariantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VentaDetalles_ProductoVariantes_ProductoVarianteId",
                table: "VentaDetalles");

            migrationBuilder.RenameColumn(
                name: "ProductoVarianteId",
                table: "VentaDetalles",
                newName: "ProductoId");

            migrationBuilder.RenameIndex(
                name: "IX_VentaDetalles_ProductoVarianteId",
                table: "VentaDetalles",
                newName: "IX_VentaDetalles_ProductoId");

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5495), new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5492) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5474), new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5471) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5578), new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5519) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5590), new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5588) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5514), new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5511) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5506), new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5504) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5967), new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5964) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5953), new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5947) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5985), new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5983) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(6003), new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5991) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5979), new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5976) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5973), new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5970) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336522"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(4979), new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(4833) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336523"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5145), new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5143) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336524"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5152), new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5150) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("42717fb8-6e3f-4c94-b6b1-a88e8718d0a6"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 29, 22, 42, 17, 743, DateTimeKind.Local).AddTicks(4377), new DateTime(2025, 11, 29, 22, 42, 17, 743, DateTimeKind.Local).AddTicks(4325) });

            migrationBuilder.AddForeignKey(
                name: "FK_VentaDetalles_Productos_ProductoId",
                table: "VentaDetalles",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
