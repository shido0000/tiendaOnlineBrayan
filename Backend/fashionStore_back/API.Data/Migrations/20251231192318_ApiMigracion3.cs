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
            migrationBuilder.AddColumn<string>(
                name: "DireccionTienda",
                table: "InformacionesGenerales",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HorarioTienda",
                table: "InformacionesGenerales",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TelefonoTienda",
                table: "InformacionesGenerales",
                type: "nvarchar(max)",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DireccionTienda",
                table: "InformacionesGenerales");

            migrationBuilder.DropColumn(
                name: "HorarioTienda",
                table: "InformacionesGenerales");

            migrationBuilder.DropColumn(
                name: "TelefonoTienda",
                table: "InformacionesGenerales");

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
        }
    }
}
