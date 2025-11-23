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
            migrationBuilder.AddColumn<string>(
                name: "Telefono",
                table: "Usuarios",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 16, 31, 26, 721, DateTimeKind.Local).AddTicks(2717), new DateTime(2025, 11, 23, 16, 31, 26, 721, DateTimeKind.Local).AddTicks(2714) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 16, 31, 26, 721, DateTimeKind.Local).AddTicks(2702), new DateTime(2025, 11, 23, 16, 31, 26, 721, DateTimeKind.Local).AddTicks(2699) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 16, 31, 26, 721, DateTimeKind.Local).AddTicks(2757), new DateTime(2025, 11, 23, 16, 31, 26, 721, DateTimeKind.Local).AddTicks(2741) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 16, 31, 26, 721, DateTimeKind.Local).AddTicks(2767), new DateTime(2025, 11, 23, 16, 31, 26, 721, DateTimeKind.Local).AddTicks(2765) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 16, 31, 26, 721, DateTimeKind.Local).AddTicks(2736), new DateTime(2025, 11, 23, 16, 31, 26, 721, DateTimeKind.Local).AddTicks(2734) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 16, 31, 26, 721, DateTimeKind.Local).AddTicks(2726), new DateTime(2025, 11, 23, 16, 31, 26, 721, DateTimeKind.Local).AddTicks(2724) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 16, 31, 26, 721, DateTimeKind.Local).AddTicks(3020), new DateTime(2025, 11, 23, 16, 31, 26, 721, DateTimeKind.Local).AddTicks(3018) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 16, 31, 26, 721, DateTimeKind.Local).AddTicks(3010), new DateTime(2025, 11, 23, 16, 31, 26, 721, DateTimeKind.Local).AddTicks(3004) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 16, 31, 26, 721, DateTimeKind.Local).AddTicks(3036), new DateTime(2025, 11, 23, 16, 31, 26, 721, DateTimeKind.Local).AddTicks(3034) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 16, 31, 26, 721, DateTimeKind.Local).AddTicks(3049), new DateTime(2025, 11, 23, 16, 31, 26, 721, DateTimeKind.Local).AddTicks(3041) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 16, 31, 26, 721, DateTimeKind.Local).AddTicks(3031), new DateTime(2025, 11, 23, 16, 31, 26, 721, DateTimeKind.Local).AddTicks(3029) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 16, 31, 26, 721, DateTimeKind.Local).AddTicks(3026), new DateTime(2025, 11, 23, 16, 31, 26, 721, DateTimeKind.Local).AddTicks(3024) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336522"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 16, 31, 26, 721, DateTimeKind.Local).AddTicks(2569), new DateTime(2025, 11, 23, 16, 31, 26, 721, DateTimeKind.Local).AddTicks(2443) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336523"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 16, 31, 26, 721, DateTimeKind.Local).AddTicks(2600), new DateTime(2025, 11, 23, 16, 31, 26, 721, DateTimeKind.Local).AddTicks(2598) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336524"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 16, 31, 26, 721, DateTimeKind.Local).AddTicks(2607), new DateTime(2025, 11, 23, 16, 31, 26, 721, DateTimeKind.Local).AddTicks(2605) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("42717fb8-6e3f-4c94-b6b1-a88e8718d0a6"),
                columns: new[] { "FechaActualizado", "FechaCreado", "Telefono" },
                values: new object[] { new DateTime(2025, 11, 23, 16, 31, 26, 722, DateTimeKind.Local).AddTicks(3445), new DateTime(2025, 11, 23, 16, 31, 26, 722, DateTimeKind.Local).AddTicks(3420), "54364363" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Telefono",
                table: "Usuarios");

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
    }
}
