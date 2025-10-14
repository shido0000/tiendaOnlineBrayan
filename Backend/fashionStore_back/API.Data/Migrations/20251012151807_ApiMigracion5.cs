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
            migrationBuilder.AddColumn<int>(
                name: "EstadoProductoInventario",
                table: "Inventarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 12, 11, 18, 7, 173, DateTimeKind.Local).AddTicks(7385), new DateTime(2025, 10, 12, 11, 18, 7, 173, DateTimeKind.Local).AddTicks(7375) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 12, 11, 18, 7, 173, DateTimeKind.Local).AddTicks(7363), new DateTime(2025, 10, 12, 11, 18, 7, 173, DateTimeKind.Local).AddTicks(7358) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 12, 11, 18, 7, 173, DateTimeKind.Local).AddTicks(7418), new DateTime(2025, 10, 12, 11, 18, 7, 173, DateTimeKind.Local).AddTicks(7415) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 12, 11, 18, 7, 173, DateTimeKind.Local).AddTicks(7599), new DateTime(2025, 10, 12, 11, 18, 7, 173, DateTimeKind.Local).AddTicks(7593) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 12, 11, 18, 7, 173, DateTimeKind.Local).AddTicks(7407), new DateTime(2025, 10, 12, 11, 18, 7, 173, DateTimeKind.Local).AddTicks(7404) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 12, 11, 18, 7, 173, DateTimeKind.Local).AddTicks(7396), new DateTime(2025, 10, 12, 11, 18, 7, 173, DateTimeKind.Local).AddTicks(7393) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 12, 11, 18, 7, 173, DateTimeKind.Local).AddTicks(8372), new DateTime(2025, 10, 12, 11, 18, 7, 173, DateTimeKind.Local).AddTicks(8362) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 12, 11, 18, 7, 173, DateTimeKind.Local).AddTicks(8339), new DateTime(2025, 10, 12, 11, 18, 7, 173, DateTimeKind.Local).AddTicks(8319) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 12, 11, 18, 7, 173, DateTimeKind.Local).AddTicks(8419), new DateTime(2025, 10, 12, 11, 18, 7, 173, DateTimeKind.Local).AddTicks(8416) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 12, 11, 18, 7, 173, DateTimeKind.Local).AddTicks(8442), new DateTime(2025, 10, 12, 11, 18, 7, 173, DateTimeKind.Local).AddTicks(8439) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 12, 11, 18, 7, 173, DateTimeKind.Local).AddTicks(8411), new DateTime(2025, 10, 12, 11, 18, 7, 173, DateTimeKind.Local).AddTicks(8405) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 12, 11, 18, 7, 173, DateTimeKind.Local).AddTicks(8393), new DateTime(2025, 10, 12, 11, 18, 7, 173, DateTimeKind.Local).AddTicks(8383) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336522"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 12, 11, 18, 7, 173, DateTimeKind.Local).AddTicks(7123), new DateTime(2025, 10, 12, 11, 18, 7, 173, DateTimeKind.Local).AddTicks(7052) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336523"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 12, 11, 18, 7, 173, DateTimeKind.Local).AddTicks(7163), new DateTime(2025, 10, 12, 11, 18, 7, 173, DateTimeKind.Local).AddTicks(7155) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336524"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 12, 11, 18, 7, 173, DateTimeKind.Local).AddTicks(7182), new DateTime(2025, 10, 12, 11, 18, 7, 173, DateTimeKind.Local).AddTicks(7173) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("42717fb8-6e3f-4c94-b6b1-a88e8718d0a6"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 12, 11, 18, 7, 176, DateTimeKind.Local).AddTicks(5505), new DateTime(2025, 10, 12, 11, 18, 7, 176, DateTimeKind.Local).AddTicks(5446) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstadoProductoInventario",
                table: "Inventarios");

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 51, 34, 725, DateTimeKind.Local).AddTicks(8292), new DateTime(2025, 10, 11, 16, 51, 34, 725, DateTimeKind.Local).AddTicks(8290) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 51, 34, 725, DateTimeKind.Local).AddTicks(8279), new DateTime(2025, 10, 11, 16, 51, 34, 725, DateTimeKind.Local).AddTicks(8276) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 51, 34, 725, DateTimeKind.Local).AddTicks(8314), new DateTime(2025, 10, 11, 16, 51, 34, 725, DateTimeKind.Local).AddTicks(8312) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 51, 34, 725, DateTimeKind.Local).AddTicks(8325), new DateTime(2025, 10, 11, 16, 51, 34, 725, DateTimeKind.Local).AddTicks(8323) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 51, 34, 725, DateTimeKind.Local).AddTicks(8307), new DateTime(2025, 10, 11, 16, 51, 34, 725, DateTimeKind.Local).AddTicks(8305) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 51, 34, 725, DateTimeKind.Local).AddTicks(8299), new DateTime(2025, 10, 11, 16, 51, 34, 725, DateTimeKind.Local).AddTicks(8297) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 51, 34, 725, DateTimeKind.Local).AddTicks(8880), new DateTime(2025, 10, 11, 16, 51, 34, 725, DateTimeKind.Local).AddTicks(8877) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 51, 34, 725, DateTimeKind.Local).AddTicks(8844), new DateTime(2025, 10, 11, 16, 51, 34, 725, DateTimeKind.Local).AddTicks(8821) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 51, 34, 725, DateTimeKind.Local).AddTicks(8907), new DateTime(2025, 10, 11, 16, 51, 34, 725, DateTimeKind.Local).AddTicks(8904) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 51, 34, 725, DateTimeKind.Local).AddTicks(8922), new DateTime(2025, 10, 11, 16, 51, 34, 725, DateTimeKind.Local).AddTicks(8919) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 51, 34, 725, DateTimeKind.Local).AddTicks(8898), new DateTime(2025, 10, 11, 16, 51, 34, 725, DateTimeKind.Local).AddTicks(8895) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 51, 34, 725, DateTimeKind.Local).AddTicks(8890), new DateTime(2025, 10, 11, 16, 51, 34, 725, DateTimeKind.Local).AddTicks(8887) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336522"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 51, 34, 725, DateTimeKind.Local).AddTicks(8140), new DateTime(2025, 10, 11, 16, 51, 34, 725, DateTimeKind.Local).AddTicks(8084) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336523"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 51, 34, 725, DateTimeKind.Local).AddTicks(8177), new DateTime(2025, 10, 11, 16, 51, 34, 725, DateTimeKind.Local).AddTicks(8175) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336524"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 51, 34, 725, DateTimeKind.Local).AddTicks(8185), new DateTime(2025, 10, 11, 16, 51, 34, 725, DateTimeKind.Local).AddTicks(8183) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("42717fb8-6e3f-4c94-b6b1-a88e8718d0a6"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 51, 34, 727, DateTimeKind.Local).AddTicks(2626), new DateTime(2025, 10, 11, 16, 51, 34, 727, DateTimeKind.Local).AddTicks(2582) });
        }
    }
}
