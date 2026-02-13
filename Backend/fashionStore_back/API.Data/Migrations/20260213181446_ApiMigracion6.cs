using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class ApiMigracion6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "CategoriaProductoId",
                table: "BannerPromocions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.UpdateData(
                table: "CuentasContables",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 13, 14, 46, 225, DateTimeKind.Local).AddTicks(2230), new DateTime(2026, 2, 13, 13, 14, 46, 225, DateTimeKind.Local).AddTicks(2173) });

            migrationBuilder.UpdateData(
                table: "CuentasContables",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 13, 14, 46, 225, DateTimeKind.Local).AddTicks(2259), new DateTime(2026, 2, 13, 13, 14, 46, 225, DateTimeKind.Local).AddTicks(2257) });

            migrationBuilder.UpdateData(
                table: "CuentasContables",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 13, 14, 46, 225, DateTimeKind.Local).AddTicks(2266), new DateTime(2026, 2, 13, 13, 14, 46, 225, DateTimeKind.Local).AddTicks(2265) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 13, 14, 46, 213, DateTimeKind.Local).AddTicks(9447), new DateTime(2026, 2, 13, 13, 14, 46, 213, DateTimeKind.Local).AddTicks(9446) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 13, 14, 46, 213, DateTimeKind.Local).AddTicks(9438), new DateTime(2026, 2, 13, 13, 14, 46, 213, DateTimeKind.Local).AddTicks(9437) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 13, 14, 46, 213, DateTimeKind.Local).AddTicks(9462), new DateTime(2026, 2, 13, 13, 14, 46, 213, DateTimeKind.Local).AddTicks(9460) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 13, 14, 46, 213, DateTimeKind.Local).AddTicks(9470), new DateTime(2026, 2, 13, 13, 14, 46, 213, DateTimeKind.Local).AddTicks(9468) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 13, 14, 46, 213, DateTimeKind.Local).AddTicks(9456), new DateTime(2026, 2, 13, 13, 14, 46, 213, DateTimeKind.Local).AddTicks(9455) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 13, 14, 46, 213, DateTimeKind.Local).AddTicks(9452), new DateTime(2026, 2, 13, 13, 14, 46, 213, DateTimeKind.Local).AddTicks(9451) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 13, 14, 46, 213, DateTimeKind.Local).AddTicks(9719), new DateTime(2026, 2, 13, 13, 14, 46, 213, DateTimeKind.Local).AddTicks(9718) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 13, 14, 46, 213, DateTimeKind.Local).AddTicks(9710), new DateTime(2026, 2, 13, 13, 14, 46, 213, DateTimeKind.Local).AddTicks(9708) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 13, 14, 46, 213, DateTimeKind.Local).AddTicks(9732), new DateTime(2026, 2, 13, 13, 14, 46, 213, DateTimeKind.Local).AddTicks(9731) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 13, 14, 46, 213, DateTimeKind.Local).AddTicks(9736), new DateTime(2026, 2, 13, 13, 14, 46, 213, DateTimeKind.Local).AddTicks(9735) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 13, 14, 46, 213, DateTimeKind.Local).AddTicks(9728), new DateTime(2026, 2, 13, 13, 14, 46, 213, DateTimeKind.Local).AddTicks(9727) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 13, 14, 46, 213, DateTimeKind.Local).AddTicks(9724), new DateTime(2026, 2, 13, 13, 14, 46, 213, DateTimeKind.Local).AddTicks(9723) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336522"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 13, 14, 46, 213, DateTimeKind.Local).AddTicks(9335), new DateTime(2026, 2, 13, 13, 14, 46, 213, DateTimeKind.Local).AddTicks(9281) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336523"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 13, 14, 46, 213, DateTimeKind.Local).AddTicks(9364), new DateTime(2026, 2, 13, 13, 14, 46, 213, DateTimeKind.Local).AddTicks(9363) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336524"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 13, 14, 46, 213, DateTimeKind.Local).AddTicks(9369), new DateTime(2026, 2, 13, 13, 14, 46, 213, DateTimeKind.Local).AddTicks(9367) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("42717fb8-6e3f-4c94-b6b1-a88e8718d0a6"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 13, 14, 46, 214, DateTimeKind.Local).AddTicks(5634), new DateTime(2026, 2, 13, 13, 14, 46, 214, DateTimeKind.Local).AddTicks(5618) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "CategoriaProductoId",
                table: "BannerPromocions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

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
    }
}
