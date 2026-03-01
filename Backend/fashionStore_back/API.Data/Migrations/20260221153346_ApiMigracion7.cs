using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class ApiMigracion7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SKUVariante",
                table: "ProductoVariantes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "CuentasContables",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 21, 10, 33, 46, 189, DateTimeKind.Local).AddTicks(6326), new DateTime(2026, 2, 21, 10, 33, 46, 189, DateTimeKind.Local).AddTicks(6272) });

            migrationBuilder.UpdateData(
                table: "CuentasContables",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 21, 10, 33, 46, 189, DateTimeKind.Local).AddTicks(6355), new DateTime(2026, 2, 21, 10, 33, 46, 189, DateTimeKind.Local).AddTicks(6354) });

            migrationBuilder.UpdateData(
                table: "CuentasContables",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 21, 10, 33, 46, 189, DateTimeKind.Local).AddTicks(6362), new DateTime(2026, 2, 21, 10, 33, 46, 189, DateTimeKind.Local).AddTicks(6361) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 21, 10, 33, 46, 177, DateTimeKind.Local).AddTicks(4173), new DateTime(2026, 2, 21, 10, 33, 46, 177, DateTimeKind.Local).AddTicks(4171) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 21, 10, 33, 46, 177, DateTimeKind.Local).AddTicks(4164), new DateTime(2026, 2, 21, 10, 33, 46, 177, DateTimeKind.Local).AddTicks(4162) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 21, 10, 33, 46, 177, DateTimeKind.Local).AddTicks(4189), new DateTime(2026, 2, 21, 10, 33, 46, 177, DateTimeKind.Local).AddTicks(4188) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 21, 10, 33, 46, 177, DateTimeKind.Local).AddTicks(4195), new DateTime(2026, 2, 21, 10, 33, 46, 177, DateTimeKind.Local).AddTicks(4194) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 21, 10, 33, 46, 177, DateTimeKind.Local).AddTicks(4183), new DateTime(2026, 2, 21, 10, 33, 46, 177, DateTimeKind.Local).AddTicks(4182) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 21, 10, 33, 46, 177, DateTimeKind.Local).AddTicks(4177), new DateTime(2026, 2, 21, 10, 33, 46, 177, DateTimeKind.Local).AddTicks(4176) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 21, 10, 33, 46, 177, DateTimeKind.Local).AddTicks(4447), new DateTime(2026, 2, 21, 10, 33, 46, 177, DateTimeKind.Local).AddTicks(4445) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 21, 10, 33, 46, 177, DateTimeKind.Local).AddTicks(4438), new DateTime(2026, 2, 21, 10, 33, 46, 177, DateTimeKind.Local).AddTicks(4436) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 21, 10, 33, 46, 177, DateTimeKind.Local).AddTicks(4459), new DateTime(2026, 2, 21, 10, 33, 46, 177, DateTimeKind.Local).AddTicks(4458) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 21, 10, 33, 46, 177, DateTimeKind.Local).AddTicks(4464), new DateTime(2026, 2, 21, 10, 33, 46, 177, DateTimeKind.Local).AddTicks(4462) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 21, 10, 33, 46, 177, DateTimeKind.Local).AddTicks(4455), new DateTime(2026, 2, 21, 10, 33, 46, 177, DateTimeKind.Local).AddTicks(4454) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 21, 10, 33, 46, 177, DateTimeKind.Local).AddTicks(4451), new DateTime(2026, 2, 21, 10, 33, 46, 177, DateTimeKind.Local).AddTicks(4450) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336522"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 21, 10, 33, 46, 177, DateTimeKind.Local).AddTicks(4066), new DateTime(2026, 2, 21, 10, 33, 46, 177, DateTimeKind.Local).AddTicks(4009) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336523"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 21, 10, 33, 46, 177, DateTimeKind.Local).AddTicks(4094), new DateTime(2026, 2, 21, 10, 33, 46, 177, DateTimeKind.Local).AddTicks(4093) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336524"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 21, 10, 33, 46, 177, DateTimeKind.Local).AddTicks(4100), new DateTime(2026, 2, 21, 10, 33, 46, 177, DateTimeKind.Local).AddTicks(4098) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("42717fb8-6e3f-4c94-b6b1-a88e8718d0a6"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 21, 10, 33, 46, 178, DateTimeKind.Local).AddTicks(1535), new DateTime(2026, 2, 21, 10, 33, 46, 178, DateTimeKind.Local).AddTicks(1518) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SKUVariante",
                table: "ProductoVariantes");

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
    }
}
