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
            migrationBuilder.UpdateData(
                table: "CuentasContables",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 12, 7, 23, 320, DateTimeKind.Local).AddTicks(5645), new DateTime(2026, 2, 13, 12, 7, 23, 320, DateTimeKind.Local).AddTicks(5588) });

            migrationBuilder.UpdateData(
                table: "CuentasContables",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 12, 7, 23, 320, DateTimeKind.Local).AddTicks(5673), new DateTime(2026, 2, 13, 12, 7, 23, 320, DateTimeKind.Local).AddTicks(5672) });

            migrationBuilder.UpdateData(
                table: "CuentasContables",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 12, 7, 23, 320, DateTimeKind.Local).AddTicks(5679), new DateTime(2026, 2, 13, 12, 7, 23, 320, DateTimeKind.Local).AddTicks(5678) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 12, 7, 23, 309, DateTimeKind.Local).AddTicks(3667), new DateTime(2026, 2, 13, 12, 7, 23, 309, DateTimeKind.Local).AddTicks(3666) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 12, 7, 23, 309, DateTimeKind.Local).AddTicks(3660), new DateTime(2026, 2, 13, 12, 7, 23, 309, DateTimeKind.Local).AddTicks(3658) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 12, 7, 23, 309, DateTimeKind.Local).AddTicks(3683), new DateTime(2026, 2, 13, 12, 7, 23, 309, DateTimeKind.Local).AddTicks(3682) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 12, 7, 23, 309, DateTimeKind.Local).AddTicks(3689), new DateTime(2026, 2, 13, 12, 7, 23, 309, DateTimeKind.Local).AddTicks(3688) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 12, 7, 23, 309, DateTimeKind.Local).AddTicks(3678), new DateTime(2026, 2, 13, 12, 7, 23, 309, DateTimeKind.Local).AddTicks(3677) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 12, 7, 23, 309, DateTimeKind.Local).AddTicks(3672), new DateTime(2026, 2, 13, 12, 7, 23, 309, DateTimeKind.Local).AddTicks(3671) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 12, 7, 23, 309, DateTimeKind.Local).AddTicks(3985), new DateTime(2026, 2, 13, 12, 7, 23, 309, DateTimeKind.Local).AddTicks(3984) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 12, 7, 23, 309, DateTimeKind.Local).AddTicks(3974), new DateTime(2026, 2, 13, 12, 7, 23, 309, DateTimeKind.Local).AddTicks(3972) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 12, 7, 23, 309, DateTimeKind.Local).AddTicks(3998), new DateTime(2026, 2, 13, 12, 7, 23, 309, DateTimeKind.Local).AddTicks(3996) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 12, 7, 23, 309, DateTimeKind.Local).AddTicks(4002), new DateTime(2026, 2, 13, 12, 7, 23, 309, DateTimeKind.Local).AddTicks(4001) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 12, 7, 23, 309, DateTimeKind.Local).AddTicks(3994), new DateTime(2026, 2, 13, 12, 7, 23, 309, DateTimeKind.Local).AddTicks(3992) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 12, 7, 23, 309, DateTimeKind.Local).AddTicks(3990), new DateTime(2026, 2, 13, 12, 7, 23, 309, DateTimeKind.Local).AddTicks(3989) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336522"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 12, 7, 23, 309, DateTimeKind.Local).AddTicks(3552), new DateTime(2026, 2, 13, 12, 7, 23, 309, DateTimeKind.Local).AddTicks(3479) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336523"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 12, 7, 23, 309, DateTimeKind.Local).AddTicks(3585), new DateTime(2026, 2, 13, 12, 7, 23, 309, DateTimeKind.Local).AddTicks(3584) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336524"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 12, 7, 23, 309, DateTimeKind.Local).AddTicks(3590), new DateTime(2026, 2, 13, 12, 7, 23, 309, DateTimeKind.Local).AddTicks(3589) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("42717fb8-6e3f-4c94-b6b1-a88e8718d0a6"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 12, 7, 23, 310, DateTimeKind.Local).AddTicks(85), new DateTime(2026, 2, 13, 12, 7, 23, 310, DateTimeKind.Local).AddTicks(66) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
