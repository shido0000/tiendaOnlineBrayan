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
            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 19, 8, 3, 28, 523, DateTimeKind.Local).AddTicks(7659), new DateTime(2025, 10, 19, 8, 3, 28, 523, DateTimeKind.Local).AddTicks(7657) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 19, 8, 3, 28, 523, DateTimeKind.Local).AddTicks(7646), new DateTime(2025, 10, 19, 8, 3, 28, 523, DateTimeKind.Local).AddTicks(7640) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 19, 8, 3, 28, 523, DateTimeKind.Local).AddTicks(7714), new DateTime(2025, 10, 19, 8, 3, 28, 523, DateTimeKind.Local).AddTicks(7686) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 19, 8, 3, 28, 523, DateTimeKind.Local).AddTicks(7727), new DateTime(2025, 10, 19, 8, 3, 28, 523, DateTimeKind.Local).AddTicks(7726) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 19, 8, 3, 28, 523, DateTimeKind.Local).AddTicks(7682), new DateTime(2025, 10, 19, 8, 3, 28, 523, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 19, 8, 3, 28, 523, DateTimeKind.Local).AddTicks(7675), new DateTime(2025, 10, 19, 8, 3, 28, 523, DateTimeKind.Local).AddTicks(7669) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 19, 8, 3, 28, 523, DateTimeKind.Local).AddTicks(7975), new DateTime(2025, 10, 19, 8, 3, 28, 523, DateTimeKind.Local).AddTicks(7973) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 19, 8, 3, 28, 523, DateTimeKind.Local).AddTicks(7966), new DateTime(2025, 10, 19, 8, 3, 28, 523, DateTimeKind.Local).AddTicks(7964) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 19, 8, 3, 28, 523, DateTimeKind.Local).AddTicks(7985), new DateTime(2025, 10, 19, 8, 3, 28, 523, DateTimeKind.Local).AddTicks(7984) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 19, 8, 3, 28, 523, DateTimeKind.Local).AddTicks(7991), new DateTime(2025, 10, 19, 8, 3, 28, 523, DateTimeKind.Local).AddTicks(7988) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 19, 8, 3, 28, 523, DateTimeKind.Local).AddTicks(7981), new DateTime(2025, 10, 19, 8, 3, 28, 523, DateTimeKind.Local).AddTicks(7980) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 19, 8, 3, 28, 523, DateTimeKind.Local).AddTicks(7978), new DateTime(2025, 10, 19, 8, 3, 28, 523, DateTimeKind.Local).AddTicks(7977) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336522"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 19, 8, 3, 28, 523, DateTimeKind.Local).AddTicks(7482), new DateTime(2025, 10, 19, 8, 3, 28, 523, DateTimeKind.Local).AddTicks(7398) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336523"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 19, 8, 3, 28, 523, DateTimeKind.Local).AddTicks(7511), new DateTime(2025, 10, 19, 8, 3, 28, 523, DateTimeKind.Local).AddTicks(7509) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336524"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 19, 8, 3, 28, 523, DateTimeKind.Local).AddTicks(7522), new DateTime(2025, 10, 19, 8, 3, 28, 523, DateTimeKind.Local).AddTicks(7516) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("42717fb8-6e3f-4c94-b6b1-a88e8718d0a6"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 19, 8, 3, 28, 524, DateTimeKind.Local).AddTicks(4660), new DateTime(2025, 10, 19, 8, 3, 28, 524, DateTimeKind.Local).AddTicks(4643) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 12, 11, 51, 56, 409, DateTimeKind.Local).AddTicks(7668), new DateTime(2025, 10, 12, 11, 51, 56, 409, DateTimeKind.Local).AddTicks(7664) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 12, 11, 51, 56, 409, DateTimeKind.Local).AddTicks(7648), new DateTime(2025, 10, 12, 11, 51, 56, 409, DateTimeKind.Local).AddTicks(7640) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 12, 11, 51, 56, 409, DateTimeKind.Local).AddTicks(7731), new DateTime(2025, 10, 12, 11, 51, 56, 409, DateTimeKind.Local).AddTicks(7708) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 12, 11, 51, 56, 409, DateTimeKind.Local).AddTicks(7931), new DateTime(2025, 10, 12, 11, 51, 56, 409, DateTimeKind.Local).AddTicks(7924) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 12, 11, 51, 56, 409, DateTimeKind.Local).AddTicks(7700), new DateTime(2025, 10, 12, 11, 51, 56, 409, DateTimeKind.Local).AddTicks(7697) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 12, 11, 51, 56, 409, DateTimeKind.Local).AddTicks(7689), new DateTime(2025, 10, 12, 11, 51, 56, 409, DateTimeKind.Local).AddTicks(7685) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 12, 11, 51, 56, 409, DateTimeKind.Local).AddTicks(8571), new DateTime(2025, 10, 12, 11, 51, 56, 409, DateTimeKind.Local).AddTicks(8567) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 12, 11, 51, 56, 409, DateTimeKind.Local).AddTicks(8551), new DateTime(2025, 10, 12, 11, 51, 56, 409, DateTimeKind.Local).AddTicks(8540) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 12, 11, 51, 56, 409, DateTimeKind.Local).AddTicks(8601), new DateTime(2025, 10, 12, 11, 51, 56, 409, DateTimeKind.Local).AddTicks(8597) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 12, 11, 51, 56, 409, DateTimeKind.Local).AddTicks(8628), new DateTime(2025, 10, 12, 11, 51, 56, 409, DateTimeKind.Local).AddTicks(8609) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 12, 11, 51, 56, 409, DateTimeKind.Local).AddTicks(8592), new DateTime(2025, 10, 12, 11, 51, 56, 409, DateTimeKind.Local).AddTicks(8588) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 12, 11, 51, 56, 409, DateTimeKind.Local).AddTicks(8583), new DateTime(2025, 10, 12, 11, 51, 56, 409, DateTimeKind.Local).AddTicks(8577) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336522"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 12, 11, 51, 56, 409, DateTimeKind.Local).AddTicks(7422), new DateTime(2025, 10, 12, 11, 51, 56, 409, DateTimeKind.Local).AddTicks(7354) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336523"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 12, 11, 51, 56, 409, DateTimeKind.Local).AddTicks(7458), new DateTime(2025, 10, 12, 11, 51, 56, 409, DateTimeKind.Local).AddTicks(7454) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336524"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 12, 11, 51, 56, 409, DateTimeKind.Local).AddTicks(7475), new DateTime(2025, 10, 12, 11, 51, 56, 409, DateTimeKind.Local).AddTicks(7465) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("42717fb8-6e3f-4c94-b6b1-a88e8718d0a6"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 12, 11, 51, 56, 411, DateTimeKind.Local).AddTicks(7800), new DateTime(2025, 10, 12, 11, 51, 56, 411, DateTimeKind.Local).AddTicks(7734) });
        }
    }
}
