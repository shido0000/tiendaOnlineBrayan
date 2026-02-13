using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class ApiMigracion5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Rebajas",
                table: "BannerPromocions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "CuentasContables",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 13, 4, 36, 241, DateTimeKind.Local).AddTicks(7197), new DateTime(2026, 2, 13, 13, 4, 36, 241, DateTimeKind.Local).AddTicks(7134) });

            migrationBuilder.UpdateData(
                table: "CuentasContables",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 13, 4, 36, 241, DateTimeKind.Local).AddTicks(7228), new DateTime(2026, 2, 13, 13, 4, 36, 241, DateTimeKind.Local).AddTicks(7227) });

            migrationBuilder.UpdateData(
                table: "CuentasContables",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 13, 4, 36, 241, DateTimeKind.Local).AddTicks(7234), new DateTime(2026, 2, 13, 13, 4, 36, 241, DateTimeKind.Local).AddTicks(7233) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 13, 4, 36, 229, DateTimeKind.Local).AddTicks(6222), new DateTime(2026, 2, 13, 13, 4, 36, 229, DateTimeKind.Local).AddTicks(6220) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 13, 4, 36, 229, DateTimeKind.Local).AddTicks(6213), new DateTime(2026, 2, 13, 13, 4, 36, 229, DateTimeKind.Local).AddTicks(6211) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 13, 4, 36, 229, DateTimeKind.Local).AddTicks(6237), new DateTime(2026, 2, 13, 13, 4, 36, 229, DateTimeKind.Local).AddTicks(6236) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 13, 4, 36, 229, DateTimeKind.Local).AddTicks(6244), new DateTime(2026, 2, 13, 13, 4, 36, 229, DateTimeKind.Local).AddTicks(6243) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 13, 4, 36, 229, DateTimeKind.Local).AddTicks(6232), new DateTime(2026, 2, 13, 13, 4, 36, 229, DateTimeKind.Local).AddTicks(6230) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 13, 4, 36, 229, DateTimeKind.Local).AddTicks(6227), new DateTime(2026, 2, 13, 13, 4, 36, 229, DateTimeKind.Local).AddTicks(6226) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 13, 4, 36, 229, DateTimeKind.Local).AddTicks(6511), new DateTime(2026, 2, 13, 13, 4, 36, 229, DateTimeKind.Local).AddTicks(6510) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 13, 4, 36, 229, DateTimeKind.Local).AddTicks(6497), new DateTime(2026, 2, 13, 13, 4, 36, 229, DateTimeKind.Local).AddTicks(6495) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 13, 4, 36, 229, DateTimeKind.Local).AddTicks(6524), new DateTime(2026, 2, 13, 13, 4, 36, 229, DateTimeKind.Local).AddTicks(6522) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 13, 4, 36, 229, DateTimeKind.Local).AddTicks(6529), new DateTime(2026, 2, 13, 13, 4, 36, 229, DateTimeKind.Local).AddTicks(6527) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 13, 4, 36, 229, DateTimeKind.Local).AddTicks(6520), new DateTime(2026, 2, 13, 13, 4, 36, 229, DateTimeKind.Local).AddTicks(6518) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 13, 4, 36, 229, DateTimeKind.Local).AddTicks(6515), new DateTime(2026, 2, 13, 13, 4, 36, 229, DateTimeKind.Local).AddTicks(6514) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336522"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 13, 4, 36, 229, DateTimeKind.Local).AddTicks(6111), new DateTime(2026, 2, 13, 13, 4, 36, 229, DateTimeKind.Local).AddTicks(6053) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336523"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 13, 4, 36, 229, DateTimeKind.Local).AddTicks(6136), new DateTime(2026, 2, 13, 13, 4, 36, 229, DateTimeKind.Local).AddTicks(6135) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336524"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 13, 4, 36, 229, DateTimeKind.Local).AddTicks(6143), new DateTime(2026, 2, 13, 13, 4, 36, 229, DateTimeKind.Local).AddTicks(6141) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("42717fb8-6e3f-4c94-b6b1-a88e8718d0a6"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 13, 4, 36, 230, DateTimeKind.Local).AddTicks(3147), new DateTime(2026, 2, 13, 13, 4, 36, 230, DateTimeKind.Local).AddTicks(3130) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rebajas",
                table: "BannerPromocions");

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
    }
}
