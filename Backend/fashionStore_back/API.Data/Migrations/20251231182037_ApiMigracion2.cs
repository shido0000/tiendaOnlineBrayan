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
            migrationBuilder.CreateTable(
                name: "InformacionesGenerales",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SobreNosotros = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Privacidad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Terminos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Devoluciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Colabora = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaActualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InformacionesGenerales", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "CuentasContables",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 13, 20, 36, 996, DateTimeKind.Local).AddTicks(5179), new DateTime(2025, 12, 31, 13, 20, 36, 996, DateTimeKind.Local).AddTicks(5121) });

            migrationBuilder.UpdateData(
                table: "CuentasContables",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 13, 20, 36, 996, DateTimeKind.Local).AddTicks(5211), new DateTime(2025, 12, 31, 13, 20, 36, 996, DateTimeKind.Local).AddTicks(5209) });

            migrationBuilder.UpdateData(
                table: "CuentasContables",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 13, 20, 36, 996, DateTimeKind.Local).AddTicks(5223), new DateTime(2025, 12, 31, 13, 20, 36, 996, DateTimeKind.Local).AddTicks(5221) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 13, 20, 36, 975, DateTimeKind.Local).AddTicks(2662), new DateTime(2025, 12, 31, 13, 20, 36, 975, DateTimeKind.Local).AddTicks(2660) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 13, 20, 36, 975, DateTimeKind.Local).AddTicks(2648), new DateTime(2025, 12, 31, 13, 20, 36, 975, DateTimeKind.Local).AddTicks(2644) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 13, 20, 36, 975, DateTimeKind.Local).AddTicks(2768), new DateTime(2025, 12, 31, 13, 20, 36, 975, DateTimeKind.Local).AddTicks(2747) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 13, 20, 36, 975, DateTimeKind.Local).AddTicks(2779), new DateTime(2025, 12, 31, 13, 20, 36, 975, DateTimeKind.Local).AddTicks(2776) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 13, 20, 36, 975, DateTimeKind.Local).AddTicks(2681), new DateTime(2025, 12, 31, 13, 20, 36, 975, DateTimeKind.Local).AddTicks(2679) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 13, 20, 36, 975, DateTimeKind.Local).AddTicks(2674), new DateTime(2025, 12, 31, 13, 20, 36, 975, DateTimeKind.Local).AddTicks(2671) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 13, 20, 36, 975, DateTimeKind.Local).AddTicks(3260), new DateTime(2025, 12, 31, 13, 20, 36, 975, DateTimeKind.Local).AddTicks(3258) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 13, 20, 36, 975, DateTimeKind.Local).AddTicks(3249), new DateTime(2025, 12, 31, 13, 20, 36, 975, DateTimeKind.Local).AddTicks(3242) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 13, 20, 36, 975, DateTimeKind.Local).AddTicks(3276), new DateTime(2025, 12, 31, 13, 20, 36, 975, DateTimeKind.Local).AddTicks(3274) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 13, 20, 36, 975, DateTimeKind.Local).AddTicks(3289), new DateTime(2025, 12, 31, 13, 20, 36, 975, DateTimeKind.Local).AddTicks(3281) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 13, 20, 36, 975, DateTimeKind.Local).AddTicks(3271), new DateTime(2025, 12, 31, 13, 20, 36, 975, DateTimeKind.Local).AddTicks(3269) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 13, 20, 36, 975, DateTimeKind.Local).AddTicks(3266), new DateTime(2025, 12, 31, 13, 20, 36, 975, DateTimeKind.Local).AddTicks(3264) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336522"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 13, 20, 36, 975, DateTimeKind.Local).AddTicks(2507), new DateTime(2025, 12, 31, 13, 20, 36, 975, DateTimeKind.Local).AddTicks(2453) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336523"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 13, 20, 36, 975, DateTimeKind.Local).AddTicks(2535), new DateTime(2025, 12, 31, 13, 20, 36, 975, DateTimeKind.Local).AddTicks(2533) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336524"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 13, 20, 36, 975, DateTimeKind.Local).AddTicks(2542), new DateTime(2025, 12, 31, 13, 20, 36, 975, DateTimeKind.Local).AddTicks(2540) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("42717fb8-6e3f-4c94-b6b1-a88e8718d0a6"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 13, 20, 36, 976, DateTimeKind.Local).AddTicks(3836), new DateTime(2025, 12, 31, 13, 20, 36, 976, DateTimeKind.Local).AddTicks(3809) });

            migrationBuilder.CreateIndex(
                name: "IX_InformacionesGenerales_Id",
                table: "InformacionesGenerales",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InformacionesGenerales");

            migrationBuilder.UpdateData(
                table: "CuentasContables",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 10, 11, 4, 247, DateTimeKind.Local).AddTicks(2448), new DateTime(2025, 12, 31, 10, 11, 4, 247, DateTimeKind.Local).AddTicks(2395) });

            migrationBuilder.UpdateData(
                table: "CuentasContables",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 10, 11, 4, 247, DateTimeKind.Local).AddTicks(2484), new DateTime(2025, 12, 31, 10, 11, 4, 247, DateTimeKind.Local).AddTicks(2482) });

            migrationBuilder.UpdateData(
                table: "CuentasContables",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 10, 11, 4, 247, DateTimeKind.Local).AddTicks(2492), new DateTime(2025, 12, 31, 10, 11, 4, 247, DateTimeKind.Local).AddTicks(2490) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 10, 11, 4, 225, DateTimeKind.Local).AddTicks(1217), new DateTime(2025, 12, 31, 10, 11, 4, 225, DateTimeKind.Local).AddTicks(1214) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 10, 11, 4, 225, DateTimeKind.Local).AddTicks(1201), new DateTime(2025, 12, 31, 10, 11, 4, 225, DateTimeKind.Local).AddTicks(1196) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 10, 11, 4, 225, DateTimeKind.Local).AddTicks(1262), new DateTime(2025, 12, 31, 10, 11, 4, 225, DateTimeKind.Local).AddTicks(1245) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 10, 11, 4, 225, DateTimeKind.Local).AddTicks(1274), new DateTime(2025, 12, 31, 10, 11, 4, 225, DateTimeKind.Local).AddTicks(1272) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 10, 11, 4, 225, DateTimeKind.Local).AddTicks(1240), new DateTime(2025, 12, 31, 10, 11, 4, 225, DateTimeKind.Local).AddTicks(1237) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 10, 11, 4, 225, DateTimeKind.Local).AddTicks(1231), new DateTime(2025, 12, 31, 10, 11, 4, 225, DateTimeKind.Local).AddTicks(1229) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 10, 11, 4, 225, DateTimeKind.Local).AddTicks(1658), new DateTime(2025, 12, 31, 10, 11, 4, 225, DateTimeKind.Local).AddTicks(1656) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 10, 11, 4, 225, DateTimeKind.Local).AddTicks(1643), new DateTime(2025, 12, 31, 10, 11, 4, 225, DateTimeKind.Local).AddTicks(1636) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 10, 11, 4, 225, DateTimeKind.Local).AddTicks(1676), new DateTime(2025, 12, 31, 10, 11, 4, 225, DateTimeKind.Local).AddTicks(1674) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 10, 11, 4, 225, DateTimeKind.Local).AddTicks(1692), new DateTime(2025, 12, 31, 10, 11, 4, 225, DateTimeKind.Local).AddTicks(1682) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 10, 11, 4, 225, DateTimeKind.Local).AddTicks(1670), new DateTime(2025, 12, 31, 10, 11, 4, 225, DateTimeKind.Local).AddTicks(1668) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 10, 11, 4, 225, DateTimeKind.Local).AddTicks(1665), new DateTime(2025, 12, 31, 10, 11, 4, 225, DateTimeKind.Local).AddTicks(1662) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336522"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 10, 11, 4, 225, DateTimeKind.Local).AddTicks(1022), new DateTime(2025, 12, 31, 10, 11, 4, 225, DateTimeKind.Local).AddTicks(961) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336523"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 10, 11, 4, 225, DateTimeKind.Local).AddTicks(1056), new DateTime(2025, 12, 31, 10, 11, 4, 225, DateTimeKind.Local).AddTicks(1054) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336524"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 10, 11, 4, 225, DateTimeKind.Local).AddTicks(1064), new DateTime(2025, 12, 31, 10, 11, 4, 225, DateTimeKind.Local).AddTicks(1062) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("42717fb8-6e3f-4c94-b6b1-a88e8718d0a6"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 10, 11, 4, 226, DateTimeKind.Local).AddTicks(4487), new DateTime(2025, 12, 31, 10, 11, 4, 226, DateTimeKind.Local).AddTicks(4441) });
        }
    }
}
