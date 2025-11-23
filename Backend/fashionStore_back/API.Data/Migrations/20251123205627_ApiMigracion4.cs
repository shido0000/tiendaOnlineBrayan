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
            migrationBuilder.AddColumn<decimal>(
                name: "PrecioAdicional",
                table: "GestorPedidos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 56, 26, 998, DateTimeKind.Local).AddTicks(496), new DateTime(2025, 11, 23, 15, 56, 26, 998, DateTimeKind.Local).AddTicks(494) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 56, 26, 998, DateTimeKind.Local).AddTicks(482), new DateTime(2025, 11, 23, 15, 56, 26, 998, DateTimeKind.Local).AddTicks(478) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 56, 26, 998, DateTimeKind.Local).AddTicks(535), new DateTime(2025, 11, 23, 15, 56, 26, 998, DateTimeKind.Local).AddTicks(519) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 56, 26, 998, DateTimeKind.Local).AddTicks(545), new DateTime(2025, 11, 23, 15, 56, 26, 998, DateTimeKind.Local).AddTicks(543) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 56, 26, 998, DateTimeKind.Local).AddTicks(514), new DateTime(2025, 11, 23, 15, 56, 26, 998, DateTimeKind.Local).AddTicks(512) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 56, 26, 998, DateTimeKind.Local).AddTicks(507), new DateTime(2025, 11, 23, 15, 56, 26, 998, DateTimeKind.Local).AddTicks(504) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 56, 26, 998, DateTimeKind.Local).AddTicks(849), new DateTime(2025, 11, 23, 15, 56, 26, 998, DateTimeKind.Local).AddTicks(846) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 56, 26, 998, DateTimeKind.Local).AddTicks(837), new DateTime(2025, 11, 23, 15, 56, 26, 998, DateTimeKind.Local).AddTicks(831) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 56, 26, 998, DateTimeKind.Local).AddTicks(865), new DateTime(2025, 11, 23, 15, 56, 26, 998, DateTimeKind.Local).AddTicks(863) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 56, 26, 998, DateTimeKind.Local).AddTicks(879), new DateTime(2025, 11, 23, 15, 56, 26, 998, DateTimeKind.Local).AddTicks(870) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 56, 26, 998, DateTimeKind.Local).AddTicks(860), new DateTime(2025, 11, 23, 15, 56, 26, 998, DateTimeKind.Local).AddTicks(857) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 56, 26, 998, DateTimeKind.Local).AddTicks(854), new DateTime(2025, 11, 23, 15, 56, 26, 998, DateTimeKind.Local).AddTicks(852) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336522"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 56, 26, 998, DateTimeKind.Local).AddTicks(333), new DateTime(2025, 11, 23, 15, 56, 26, 998, DateTimeKind.Local).AddTicks(152) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336523"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 56, 26, 998, DateTimeKind.Local).AddTicks(367), new DateTime(2025, 11, 23, 15, 56, 26, 998, DateTimeKind.Local).AddTicks(365) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336524"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 56, 26, 998, DateTimeKind.Local).AddTicks(374), new DateTime(2025, 11, 23, 15, 56, 26, 998, DateTimeKind.Local).AddTicks(372) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("42717fb8-6e3f-4c94-b6b1-a88e8718d0a6"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 56, 27, 0, DateTimeKind.Local).AddTicks(305), new DateTime(2025, 11, 23, 15, 56, 27, 0, DateTimeKind.Local).AddTicks(241) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrecioAdicional",
                table: "GestorPedidos");

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 52, 20, 365, DateTimeKind.Local).AddTicks(2027), new DateTime(2025, 11, 23, 15, 52, 20, 365, DateTimeKind.Local).AddTicks(2025) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 52, 20, 365, DateTimeKind.Local).AddTicks(2018), new DateTime(2025, 11, 23, 15, 52, 20, 365, DateTimeKind.Local).AddTicks(2016) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 52, 20, 365, DateTimeKind.Local).AddTicks(2046), new DateTime(2025, 11, 23, 15, 52, 20, 365, DateTimeKind.Local).AddTicks(2044) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 52, 20, 365, DateTimeKind.Local).AddTicks(2054), new DateTime(2025, 11, 23, 15, 52, 20, 365, DateTimeKind.Local).AddTicks(2052) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 52, 20, 365, DateTimeKind.Local).AddTicks(2040), new DateTime(2025, 11, 23, 15, 52, 20, 365, DateTimeKind.Local).AddTicks(2038) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 52, 20, 365, DateTimeKind.Local).AddTicks(2033), new DateTime(2025, 11, 23, 15, 52, 20, 365, DateTimeKind.Local).AddTicks(2032) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 52, 20, 365, DateTimeKind.Local).AddTicks(2302), new DateTime(2025, 11, 23, 15, 52, 20, 365, DateTimeKind.Local).AddTicks(2300) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 52, 20, 365, DateTimeKind.Local).AddTicks(2292), new DateTime(2025, 11, 23, 15, 52, 20, 365, DateTimeKind.Local).AddTicks(2287) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 52, 20, 365, DateTimeKind.Local).AddTicks(2316), new DateTime(2025, 11, 23, 15, 52, 20, 365, DateTimeKind.Local).AddTicks(2314) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 52, 20, 365, DateTimeKind.Local).AddTicks(2322), new DateTime(2025, 11, 23, 15, 52, 20, 365, DateTimeKind.Local).AddTicks(2320) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 52, 20, 365, DateTimeKind.Local).AddTicks(2311), new DateTime(2025, 11, 23, 15, 52, 20, 365, DateTimeKind.Local).AddTicks(2310) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 52, 20, 365, DateTimeKind.Local).AddTicks(2307), new DateTime(2025, 11, 23, 15, 52, 20, 365, DateTimeKind.Local).AddTicks(2305) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336522"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 52, 20, 365, DateTimeKind.Local).AddTicks(1906), new DateTime(2025, 11, 23, 15, 52, 20, 365, DateTimeKind.Local).AddTicks(1860) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336523"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 52, 20, 365, DateTimeKind.Local).AddTicks(1933), new DateTime(2025, 11, 23, 15, 52, 20, 365, DateTimeKind.Local).AddTicks(1931) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336524"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 52, 20, 365, DateTimeKind.Local).AddTicks(1939), new DateTime(2025, 11, 23, 15, 52, 20, 365, DateTimeKind.Local).AddTicks(1937) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("42717fb8-6e3f-4c94-b6b1-a88e8718d0a6"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 15, 52, 20, 384, DateTimeKind.Local).AddTicks(2826), new DateTime(2025, 11, 23, 15, 52, 20, 384, DateTimeKind.Local).AddTicks(2758) });
        }
    }
}
