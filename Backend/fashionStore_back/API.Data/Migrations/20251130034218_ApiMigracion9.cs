using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class ApiMigracion9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Codigo",
                table: "Pedidos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5495), new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5492) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5474), new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5471) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5578), new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5519) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5590), new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5588) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5514), new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5511) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5506), new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5504) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5967), new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5964) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5953), new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5947) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5985), new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5983) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(6003), new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5991) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5979), new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5976) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5973), new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5970) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336522"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(4979), new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(4833) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336523"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5145), new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5143) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336524"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5152), new DateTime(2025, 11, 29, 22, 42, 17, 741, DateTimeKind.Local).AddTicks(5150) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("42717fb8-6e3f-4c94-b6b1-a88e8718d0a6"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 29, 22, 42, 17, 743, DateTimeKind.Local).AddTicks(4377), new DateTime(2025, 11, 29, 22, 42, 17, 743, DateTimeKind.Local).AddTicks(4325) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "Pedidos");

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 29, 22, 6, 30, 258, DateTimeKind.Local).AddTicks(7057), new DateTime(2025, 11, 29, 22, 6, 30, 258, DateTimeKind.Local).AddTicks(7054) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 29, 22, 6, 30, 258, DateTimeKind.Local).AddTicks(7043), new DateTime(2025, 11, 29, 22, 6, 30, 258, DateTimeKind.Local).AddTicks(7039) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 29, 22, 6, 30, 258, DateTimeKind.Local).AddTicks(7099), new DateTime(2025, 11, 29, 22, 6, 30, 258, DateTimeKind.Local).AddTicks(7079) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 29, 22, 6, 30, 258, DateTimeKind.Local).AddTicks(7108), new DateTime(2025, 11, 29, 22, 6, 30, 258, DateTimeKind.Local).AddTicks(7105) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 29, 22, 6, 30, 258, DateTimeKind.Local).AddTicks(7074), new DateTime(2025, 11, 29, 22, 6, 30, 258, DateTimeKind.Local).AddTicks(7072) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 29, 22, 6, 30, 258, DateTimeKind.Local).AddTicks(7067), new DateTime(2025, 11, 29, 22, 6, 30, 258, DateTimeKind.Local).AddTicks(7064) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 29, 22, 6, 30, 258, DateTimeKind.Local).AddTicks(7589), new DateTime(2025, 11, 29, 22, 6, 30, 258, DateTimeKind.Local).AddTicks(7586) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 29, 22, 6, 30, 258, DateTimeKind.Local).AddTicks(7578), new DateTime(2025, 11, 29, 22, 6, 30, 258, DateTimeKind.Local).AddTicks(7572) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 29, 22, 6, 30, 258, DateTimeKind.Local).AddTicks(7672), new DateTime(2025, 11, 29, 22, 6, 30, 258, DateTimeKind.Local).AddTicks(7670) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 29, 22, 6, 30, 258, DateTimeKind.Local).AddTicks(7687), new DateTime(2025, 11, 29, 22, 6, 30, 258, DateTimeKind.Local).AddTicks(7677) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 29, 22, 6, 30, 258, DateTimeKind.Local).AddTicks(7665), new DateTime(2025, 11, 29, 22, 6, 30, 258, DateTimeKind.Local).AddTicks(7662) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 29, 22, 6, 30, 258, DateTimeKind.Local).AddTicks(7595), new DateTime(2025, 11, 29, 22, 6, 30, 258, DateTimeKind.Local).AddTicks(7593) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336522"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 29, 22, 6, 30, 258, DateTimeKind.Local).AddTicks(6875), new DateTime(2025, 11, 29, 22, 6, 30, 258, DateTimeKind.Local).AddTicks(6724) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336523"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 29, 22, 6, 30, 258, DateTimeKind.Local).AddTicks(6911), new DateTime(2025, 11, 29, 22, 6, 30, 258, DateTimeKind.Local).AddTicks(6909) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336524"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 29, 22, 6, 30, 258, DateTimeKind.Local).AddTicks(6918), new DateTime(2025, 11, 29, 22, 6, 30, 258, DateTimeKind.Local).AddTicks(6916) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("42717fb8-6e3f-4c94-b6b1-a88e8718d0a6"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 29, 22, 6, 30, 261, DateTimeKind.Local).AddTicks(4907), new DateTime(2025, 11, 29, 22, 6, 30, 261, DateTimeKind.Local).AddTicks(4839) });
        }
    }
}
