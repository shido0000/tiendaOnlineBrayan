using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class ApiMigracion3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ubicaciones",
                table: "BannerPromocions");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoriaProductoId",
                table: "BannerPromocions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "CuentasContables",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 12, 2, 49, 551, DateTimeKind.Local).AddTicks(202), new DateTime(2026, 2, 13, 12, 2, 49, 551, DateTimeKind.Local).AddTicks(146) });

            migrationBuilder.UpdateData(
                table: "CuentasContables",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 12, 2, 49, 551, DateTimeKind.Local).AddTicks(232), new DateTime(2026, 2, 13, 12, 2, 49, 551, DateTimeKind.Local).AddTicks(230) });

            migrationBuilder.UpdateData(
                table: "CuentasContables",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 12, 2, 49, 551, DateTimeKind.Local).AddTicks(238), new DateTime(2026, 2, 13, 12, 2, 49, 551, DateTimeKind.Local).AddTicks(237) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 12, 2, 49, 538, DateTimeKind.Local).AddTicks(5747), new DateTime(2026, 2, 13, 12, 2, 49, 538, DateTimeKind.Local).AddTicks(5746) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 12, 2, 49, 538, DateTimeKind.Local).AddTicks(5739), new DateTime(2026, 2, 13, 12, 2, 49, 538, DateTimeKind.Local).AddTicks(5737) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 12, 2, 49, 538, DateTimeKind.Local).AddTicks(5762), new DateTime(2026, 2, 13, 12, 2, 49, 538, DateTimeKind.Local).AddTicks(5761) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 12, 2, 49, 538, DateTimeKind.Local).AddTicks(5864), new DateTime(2026, 2, 13, 12, 2, 49, 538, DateTimeKind.Local).AddTicks(5858) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 12, 2, 49, 538, DateTimeKind.Local).AddTicks(5758), new DateTime(2026, 2, 13, 12, 2, 49, 538, DateTimeKind.Local).AddTicks(5757) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 12, 2, 49, 538, DateTimeKind.Local).AddTicks(5752), new DateTime(2026, 2, 13, 12, 2, 49, 538, DateTimeKind.Local).AddTicks(5751) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 12, 2, 49, 538, DateTimeKind.Local).AddTicks(6185), new DateTime(2026, 2, 13, 12, 2, 49, 538, DateTimeKind.Local).AddTicks(6184) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 12, 2, 49, 538, DateTimeKind.Local).AddTicks(6175), new DateTime(2026, 2, 13, 12, 2, 49, 538, DateTimeKind.Local).AddTicks(6172) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 12, 2, 49, 538, DateTimeKind.Local).AddTicks(6198), new DateTime(2026, 2, 13, 12, 2, 49, 538, DateTimeKind.Local).AddTicks(6197) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 12, 2, 49, 538, DateTimeKind.Local).AddTicks(6203), new DateTime(2026, 2, 13, 12, 2, 49, 538, DateTimeKind.Local).AddTicks(6201) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 12, 2, 49, 538, DateTimeKind.Local).AddTicks(6194), new DateTime(2026, 2, 13, 12, 2, 49, 538, DateTimeKind.Local).AddTicks(6192) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 12, 2, 49, 538, DateTimeKind.Local).AddTicks(6190), new DateTime(2026, 2, 13, 12, 2, 49, 538, DateTimeKind.Local).AddTicks(6189) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336522"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 12, 2, 49, 538, DateTimeKind.Local).AddTicks(5626), new DateTime(2026, 2, 13, 12, 2, 49, 538, DateTimeKind.Local).AddTicks(5559) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336523"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 12, 2, 49, 538, DateTimeKind.Local).AddTicks(5657), new DateTime(2026, 2, 13, 12, 2, 49, 538, DateTimeKind.Local).AddTicks(5655) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336524"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 12, 2, 49, 538, DateTimeKind.Local).AddTicks(5664), new DateTime(2026, 2, 13, 12, 2, 49, 538, DateTimeKind.Local).AddTicks(5662) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("42717fb8-6e3f-4c94-b6b1-a88e8718d0a6"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 12, 2, 49, 539, DateTimeKind.Local).AddTicks(3677), new DateTime(2026, 2, 13, 12, 2, 49, 539, DateTimeKind.Local).AddTicks(3651) });

            migrationBuilder.CreateIndex(
                name: "IX_BannerPromocions_CategoriaProductoId",
                table: "BannerPromocions",
                column: "CategoriaProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_BannerPromocions_CategoriasProductos_CategoriaProductoId",
                table: "BannerPromocions",
                column: "CategoriaProductoId",
                principalTable: "CategoriasProductos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BannerPromocions_CategoriasProductos_CategoriaProductoId",
                table: "BannerPromocions");

            migrationBuilder.DropIndex(
                name: "IX_BannerPromocions_CategoriaProductoId",
                table: "BannerPromocions");

            migrationBuilder.DropColumn(
                name: "CategoriaProductoId",
                table: "BannerPromocions");

            migrationBuilder.AddColumn<string>(
                name: "Ubicaciones",
                table: "BannerPromocions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "CuentasContables",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 11, 15, 47, 867, DateTimeKind.Local).AddTicks(8117), new DateTime(2026, 2, 13, 11, 15, 47, 867, DateTimeKind.Local).AddTicks(8044) });

            migrationBuilder.UpdateData(
                table: "CuentasContables",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 11, 15, 47, 867, DateTimeKind.Local).AddTicks(8167), new DateTime(2026, 2, 13, 11, 15, 47, 867, DateTimeKind.Local).AddTicks(8163) });

            migrationBuilder.UpdateData(
                table: "CuentasContables",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 11, 15, 47, 867, DateTimeKind.Local).AddTicks(8175), new DateTime(2026, 2, 13, 11, 15, 47, 867, DateTimeKind.Local).AddTicks(8173) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6408), new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6406) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6400), new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6398) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6507), new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6505) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6515), new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6513) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6417), new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6416) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6412), new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6411) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6755), new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6753) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6746), new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6743) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6767), new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6766) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6772), new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6770) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6763), new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6762) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6759), new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6758) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336522"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6300), new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6245) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336523"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6331), new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6329) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336524"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6337), new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6335) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("42717fb8-6e3f-4c94-b6b1-a88e8718d0a6"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 11, 15, 47, 856, DateTimeKind.Local).AddTicks(3275), new DateTime(2026, 2, 13, 11, 15, 47, 856, DateTimeKind.Local).AddTicks(3255) });
        }
    }
}
