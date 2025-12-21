using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class ApiMigracion2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BannerPromocions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Imagen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TextoTitulo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TextoSubtitulo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BotonTexto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BotonVinculo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EsActivo = table.Column<bool>(type: "bit", nullable: false),
                    Destacado = table.Column<bool>(type: "bit", nullable: false),
                    Orden = table.Column<int>(type: "int", nullable: false),
                    Ubicaciones = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dispositivos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaCreado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaActualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BannerPromocions", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 16, 22, 34, 4, 904, DateTimeKind.Local).AddTicks(7112), new DateTime(2025, 12, 16, 22, 34, 4, 904, DateTimeKind.Local).AddTicks(7110) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 16, 22, 34, 4, 904, DateTimeKind.Local).AddTicks(7103), new DateTime(2025, 12, 16, 22, 34, 4, 904, DateTimeKind.Local).AddTicks(7101) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 16, 22, 34, 4, 904, DateTimeKind.Local).AddTicks(7127), new DateTime(2025, 12, 16, 22, 34, 4, 904, DateTimeKind.Local).AddTicks(7126) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 16, 22, 34, 4, 904, DateTimeKind.Local).AddTicks(7134), new DateTime(2025, 12, 16, 22, 34, 4, 904, DateTimeKind.Local).AddTicks(7133) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 16, 22, 34, 4, 904, DateTimeKind.Local).AddTicks(7122), new DateTime(2025, 12, 16, 22, 34, 4, 904, DateTimeKind.Local).AddTicks(7121) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 16, 22, 34, 4, 904, DateTimeKind.Local).AddTicks(7116), new DateTime(2025, 12, 16, 22, 34, 4, 904, DateTimeKind.Local).AddTicks(7115) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 16, 22, 34, 4, 904, DateTimeKind.Local).AddTicks(7365), new DateTime(2025, 12, 16, 22, 34, 4, 904, DateTimeKind.Local).AddTicks(7364) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 16, 22, 34, 4, 904, DateTimeKind.Local).AddTicks(7354), new DateTime(2025, 12, 16, 22, 34, 4, 904, DateTimeKind.Local).AddTicks(7352) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 16, 22, 34, 4, 904, DateTimeKind.Local).AddTicks(7378), new DateTime(2025, 12, 16, 22, 34, 4, 904, DateTimeKind.Local).AddTicks(7376) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 16, 22, 34, 4, 904, DateTimeKind.Local).AddTicks(7382), new DateTime(2025, 12, 16, 22, 34, 4, 904, DateTimeKind.Local).AddTicks(7381) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 16, 22, 34, 4, 904, DateTimeKind.Local).AddTicks(7374), new DateTime(2025, 12, 16, 22, 34, 4, 904, DateTimeKind.Local).AddTicks(7373) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 16, 22, 34, 4, 904, DateTimeKind.Local).AddTicks(7370), new DateTime(2025, 12, 16, 22, 34, 4, 904, DateTimeKind.Local).AddTicks(7368) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336522"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 16, 22, 34, 4, 904, DateTimeKind.Local).AddTicks(6999), new DateTime(2025, 12, 16, 22, 34, 4, 904, DateTimeKind.Local).AddTicks(6940) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336523"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 16, 22, 34, 4, 904, DateTimeKind.Local).AddTicks(7031), new DateTime(2025, 12, 16, 22, 34, 4, 904, DateTimeKind.Local).AddTicks(7029) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336524"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 16, 22, 34, 4, 904, DateTimeKind.Local).AddTicks(7037), new DateTime(2025, 12, 16, 22, 34, 4, 904, DateTimeKind.Local).AddTicks(7035) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("42717fb8-6e3f-4c94-b6b1-a88e8718d0a6"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 16, 22, 34, 4, 905, DateTimeKind.Local).AddTicks(3576), new DateTime(2025, 12, 16, 22, 34, 4, 905, DateTimeKind.Local).AddTicks(3558) });

            migrationBuilder.CreateIndex(
                name: "IX_BannerPromocions_Id",
                table: "BannerPromocions",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BannerPromocions");

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 15, 12, 14, 40, 228, DateTimeKind.Local).AddTicks(2151), new DateTime(2025, 12, 15, 12, 14, 40, 228, DateTimeKind.Local).AddTicks(2149) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 15, 12, 14, 40, 228, DateTimeKind.Local).AddTicks(2137), new DateTime(2025, 12, 15, 12, 14, 40, 228, DateTimeKind.Local).AddTicks(2134) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 15, 12, 14, 40, 228, DateTimeKind.Local).AddTicks(2194), new DateTime(2025, 12, 15, 12, 14, 40, 228, DateTimeKind.Local).AddTicks(2173) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 15, 12, 14, 40, 228, DateTimeKind.Local).AddTicks(2202), new DateTime(2025, 12, 15, 12, 14, 40, 228, DateTimeKind.Local).AddTicks(2200) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 15, 12, 14, 40, 228, DateTimeKind.Local).AddTicks(2169), new DateTime(2025, 12, 15, 12, 14, 40, 228, DateTimeKind.Local).AddTicks(2168) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 15, 12, 14, 40, 228, DateTimeKind.Local).AddTicks(2164), new DateTime(2025, 12, 15, 12, 14, 40, 228, DateTimeKind.Local).AddTicks(2158) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 15, 12, 14, 40, 228, DateTimeKind.Local).AddTicks(2410), new DateTime(2025, 12, 15, 12, 14, 40, 228, DateTimeKind.Local).AddTicks(2408) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 15, 12, 14, 40, 228, DateTimeKind.Local).AddTicks(2398), new DateTime(2025, 12, 15, 12, 14, 40, 228, DateTimeKind.Local).AddTicks(2395) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 15, 12, 14, 40, 228, DateTimeKind.Local).AddTicks(2424), new DateTime(2025, 12, 15, 12, 14, 40, 228, DateTimeKind.Local).AddTicks(2423) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 15, 12, 14, 40, 228, DateTimeKind.Local).AddTicks(2432), new DateTime(2025, 12, 15, 12, 14, 40, 228, DateTimeKind.Local).AddTicks(2428) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 15, 12, 14, 40, 228, DateTimeKind.Local).AddTicks(2420), new DateTime(2025, 12, 15, 12, 14, 40, 228, DateTimeKind.Local).AddTicks(2418) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 15, 12, 14, 40, 228, DateTimeKind.Local).AddTicks(2415), new DateTime(2025, 12, 15, 12, 14, 40, 228, DateTimeKind.Local).AddTicks(2413) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336522"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 15, 12, 14, 40, 228, DateTimeKind.Local).AddTicks(2003), new DateTime(2025, 12, 15, 12, 14, 40, 228, DateTimeKind.Local).AddTicks(1935) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336523"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 15, 12, 14, 40, 228, DateTimeKind.Local).AddTicks(2034), new DateTime(2025, 12, 15, 12, 14, 40, 228, DateTimeKind.Local).AddTicks(2032) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336524"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 15, 12, 14, 40, 228, DateTimeKind.Local).AddTicks(2039), new DateTime(2025, 12, 15, 12, 14, 40, 228, DateTimeKind.Local).AddTicks(2037) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("42717fb8-6e3f-4c94-b6b1-a88e8718d0a6"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 12, 15, 12, 14, 40, 229, DateTimeKind.Local).AddTicks(2897), new DateTime(2025, 12, 15, 12, 14, 40, 229, DateTimeKind.Local).AddTicks(2870) });
        }
    }
}
