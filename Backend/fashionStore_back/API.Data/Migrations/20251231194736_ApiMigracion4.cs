using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class ApiMigracion4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EnlaceFacebook",
                table: "InformacionesGenerales",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnlaceInstagram",
                table: "InformacionesGenerales",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnlaceTelegram",
                table: "InformacionesGenerales",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnlaceWhatsapp",
                table: "InformacionesGenerales",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "CuentasContables",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 14, 47, 35, 796, DateTimeKind.Local).AddTicks(3449), new DateTime(2025, 12, 31, 14, 47, 35, 796, DateTimeKind.Local).AddTicks(3386) });

            migrationBuilder.UpdateData(
                table: "CuentasContables",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 14, 47, 35, 796, DateTimeKind.Local).AddTicks(3512), new DateTime(2025, 12, 31, 14, 47, 35, 796, DateTimeKind.Local).AddTicks(3495) });

            migrationBuilder.UpdateData(
                table: "CuentasContables",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 14, 47, 35, 796, DateTimeKind.Local).AddTicks(3531), new DateTime(2025, 12, 31, 14, 47, 35, 796, DateTimeKind.Local).AddTicks(3527) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 14, 47, 35, 755, DateTimeKind.Local).AddTicks(237), new DateTime(2025, 12, 31, 14, 47, 35, 755, DateTimeKind.Local).AddTicks(235) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 14, 47, 35, 755, DateTimeKind.Local).AddTicks(227), new DateTime(2025, 12, 31, 14, 47, 35, 755, DateTimeKind.Local).AddTicks(223) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 14, 47, 35, 755, DateTimeKind.Local).AddTicks(259), new DateTime(2025, 12, 31, 14, 47, 35, 755, DateTimeKind.Local).AddTicks(257) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 14, 47, 35, 755, DateTimeKind.Local).AddTicks(338), new DateTime(2025, 12, 31, 14, 47, 35, 755, DateTimeKind.Local).AddTicks(335) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 14, 47, 35, 755, DateTimeKind.Local).AddTicks(252), new DateTime(2025, 12, 31, 14, 47, 35, 755, DateTimeKind.Local).AddTicks(250) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 14, 47, 35, 755, DateTimeKind.Local).AddTicks(245), new DateTime(2025, 12, 31, 14, 47, 35, 755, DateTimeKind.Local).AddTicks(242) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 14, 47, 35, 755, DateTimeKind.Local).AddTicks(1555), new DateTime(2025, 12, 31, 14, 47, 35, 755, DateTimeKind.Local).AddTicks(1542) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 14, 47, 35, 755, DateTimeKind.Local).AddTicks(1471), new DateTime(2025, 12, 31, 14, 47, 35, 755, DateTimeKind.Local).AddTicks(1353) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 14, 47, 35, 755, DateTimeKind.Local).AddTicks(1767), new DateTime(2025, 12, 31, 14, 47, 35, 755, DateTimeKind.Local).AddTicks(1764) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 14, 47, 35, 755, DateTimeKind.Local).AddTicks(1793), new DateTime(2025, 12, 31, 14, 47, 35, 755, DateTimeKind.Local).AddTicks(1789) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 14, 47, 35, 755, DateTimeKind.Local).AddTicks(1757), new DateTime(2025, 12, 31, 14, 47, 35, 755, DateTimeKind.Local).AddTicks(1752) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 14, 47, 35, 755, DateTimeKind.Local).AddTicks(1698), new DateTime(2025, 12, 31, 14, 47, 35, 755, DateTimeKind.Local).AddTicks(1581) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336522"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 14, 47, 35, 755, DateTimeKind.Local).AddTicks(100), new DateTime(2025, 12, 31, 14, 47, 35, 755, DateTimeKind.Local).AddTicks(46) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336523"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 14, 47, 35, 755, DateTimeKind.Local).AddTicks(126), new DateTime(2025, 12, 31, 14, 47, 35, 755, DateTimeKind.Local).AddTicks(124) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336524"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 14, 47, 35, 755, DateTimeKind.Local).AddTicks(134), new DateTime(2025, 12, 31, 14, 47, 35, 755, DateTimeKind.Local).AddTicks(132) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("42717fb8-6e3f-4c94-b6b1-a88e8718d0a6"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 14, 47, 35, 756, DateTimeKind.Local).AddTicks(7745), new DateTime(2025, 12, 31, 14, 47, 35, 756, DateTimeKind.Local).AddTicks(7698) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnlaceFacebook",
                table: "InformacionesGenerales");

            migrationBuilder.DropColumn(
                name: "EnlaceInstagram",
                table: "InformacionesGenerales");

            migrationBuilder.DropColumn(
                name: "EnlaceTelegram",
                table: "InformacionesGenerales");

            migrationBuilder.DropColumn(
                name: "EnlaceWhatsapp",
                table: "InformacionesGenerales");

            migrationBuilder.UpdateData(
                table: "CuentasContables",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 14, 23, 17, 557, DateTimeKind.Local).AddTicks(929), new DateTime(2025, 12, 31, 14, 23, 17, 557, DateTimeKind.Local).AddTicks(805) });

            migrationBuilder.UpdateData(
                table: "CuentasContables",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 14, 23, 17, 557, DateTimeKind.Local).AddTicks(965), new DateTime(2025, 12, 31, 14, 23, 17, 557, DateTimeKind.Local).AddTicks(962) });

            migrationBuilder.UpdateData(
                table: "CuentasContables",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 14, 23, 17, 557, DateTimeKind.Local).AddTicks(973), new DateTime(2025, 12, 31, 14, 23, 17, 557, DateTimeKind.Local).AddTicks(971) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 14, 23, 17, 534, DateTimeKind.Local).AddTicks(2650), new DateTime(2025, 12, 31, 14, 23, 17, 534, DateTimeKind.Local).AddTicks(2632) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 14, 23, 17, 534, DateTimeKind.Local).AddTicks(2567), new DateTime(2025, 12, 31, 14, 23, 17, 534, DateTimeKind.Local).AddTicks(2543) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 14, 23, 17, 534, DateTimeKind.Local).AddTicks(2706), new DateTime(2025, 12, 31, 14, 23, 17, 534, DateTimeKind.Local).AddTicks(2686) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 14, 23, 17, 534, DateTimeKind.Local).AddTicks(2721), new DateTime(2025, 12, 31, 14, 23, 17, 534, DateTimeKind.Local).AddTicks(2718) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 14, 23, 17, 534, DateTimeKind.Local).AddTicks(2680), new DateTime(2025, 12, 31, 14, 23, 17, 534, DateTimeKind.Local).AddTicks(2678) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 14, 23, 17, 534, DateTimeKind.Local).AddTicks(2673), new DateTime(2025, 12, 31, 14, 23, 17, 534, DateTimeKind.Local).AddTicks(2669) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 14, 23, 17, 534, DateTimeKind.Local).AddTicks(3417), new DateTime(2025, 12, 31, 14, 23, 17, 534, DateTimeKind.Local).AddTicks(3414) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 14, 23, 17, 534, DateTimeKind.Local).AddTicks(3403), new DateTime(2025, 12, 31, 14, 23, 17, 534, DateTimeKind.Local).AddTicks(3396) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 14, 23, 17, 534, DateTimeKind.Local).AddTicks(3435), new DateTime(2025, 12, 31, 14, 23, 17, 534, DateTimeKind.Local).AddTicks(3433) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 14, 23, 17, 534, DateTimeKind.Local).AddTicks(3452), new DateTime(2025, 12, 31, 14, 23, 17, 534, DateTimeKind.Local).AddTicks(3442) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 14, 23, 17, 534, DateTimeKind.Local).AddTicks(3430), new DateTime(2025, 12, 31, 14, 23, 17, 534, DateTimeKind.Local).AddTicks(3427) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 14, 23, 17, 534, DateTimeKind.Local).AddTicks(3424), new DateTime(2025, 12, 31, 14, 23, 17, 534, DateTimeKind.Local).AddTicks(3421) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336522"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 14, 23, 17, 534, DateTimeKind.Local).AddTicks(2220), new DateTime(2025, 12, 31, 14, 23, 17, 534, DateTimeKind.Local).AddTicks(2145) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336523"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 14, 23, 17, 534, DateTimeKind.Local).AddTicks(2279), new DateTime(2025, 12, 31, 14, 23, 17, 534, DateTimeKind.Local).AddTicks(2273) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336524"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 14, 23, 17, 534, DateTimeKind.Local).AddTicks(2292), new DateTime(2025, 12, 31, 14, 23, 17, 534, DateTimeKind.Local).AddTicks(2288) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("42717fb8-6e3f-4c94-b6b1-a88e8718d0a6"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 31, 14, 23, 17, 535, DateTimeKind.Local).AddTicks(7544), new DateTime(2025, 12, 31, 14, 23, 17, 535, DateTimeKind.Local).AddTicks(7492) });
        }
    }
}
