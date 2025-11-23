using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class ApiMigracion3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mensajerias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Precio = table.Column<int>(type: "int", nullable: false),
                    MonedaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaActualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mensajerias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mensajerias_Monedas_MonedaId",
                        column: x => x.MonedaId,
                        principalTable: "Monedas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Mensajerias_Id",
                table: "Mensajerias",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mensajerias_MonedaId",
                table: "Mensajerias",
                column: "MonedaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mensajerias");

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 8, 58, 23, 428, DateTimeKind.Local).AddTicks(8049), new DateTime(2025, 11, 23, 8, 58, 23, 428, DateTimeKind.Local).AddTicks(8047) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 8, 58, 23, 428, DateTimeKind.Local).AddTicks(8036), new DateTime(2025, 11, 23, 8, 58, 23, 428, DateTimeKind.Local).AddTicks(8033) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 8, 58, 23, 428, DateTimeKind.Local).AddTicks(8073), new DateTime(2025, 11, 23, 8, 58, 23, 428, DateTimeKind.Local).AddTicks(8070) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 8, 58, 23, 428, DateTimeKind.Local).AddTicks(8082), new DateTime(2025, 11, 23, 8, 58, 23, 428, DateTimeKind.Local).AddTicks(8079) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 8, 58, 23, 428, DateTimeKind.Local).AddTicks(8065), new DateTime(2025, 11, 23, 8, 58, 23, 428, DateTimeKind.Local).AddTicks(8063) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 8, 58, 23, 428, DateTimeKind.Local).AddTicks(8057), new DateTime(2025, 11, 23, 8, 58, 23, 428, DateTimeKind.Local).AddTicks(8055) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 8, 58, 23, 428, DateTimeKind.Local).AddTicks(8457), new DateTime(2025, 11, 23, 8, 58, 23, 428, DateTimeKind.Local).AddTicks(8454) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 8, 58, 23, 428, DateTimeKind.Local).AddTicks(8445), new DateTime(2025, 11, 23, 8, 58, 23, 428, DateTimeKind.Local).AddTicks(8441) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 8, 58, 23, 428, DateTimeKind.Local).AddTicks(8476), new DateTime(2025, 11, 23, 8, 58, 23, 428, DateTimeKind.Local).AddTicks(8474) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 8, 58, 23, 428, DateTimeKind.Local).AddTicks(8483), new DateTime(2025, 11, 23, 8, 58, 23, 428, DateTimeKind.Local).AddTicks(8481) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 8, 58, 23, 428, DateTimeKind.Local).AddTicks(8470), new DateTime(2025, 11, 23, 8, 58, 23, 428, DateTimeKind.Local).AddTicks(8467) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 8, 58, 23, 428, DateTimeKind.Local).AddTicks(8463), new DateTime(2025, 11, 23, 8, 58, 23, 428, DateTimeKind.Local).AddTicks(8461) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336522"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 8, 58, 23, 428, DateTimeKind.Local).AddTicks(7855), new DateTime(2025, 11, 23, 8, 58, 23, 428, DateTimeKind.Local).AddTicks(7798) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336523"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 8, 58, 23, 428, DateTimeKind.Local).AddTicks(7884), new DateTime(2025, 11, 23, 8, 58, 23, 428, DateTimeKind.Local).AddTicks(7882) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336524"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 8, 58, 23, 428, DateTimeKind.Local).AddTicks(7892), new DateTime(2025, 11, 23, 8, 58, 23, 428, DateTimeKind.Local).AddTicks(7890) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("42717fb8-6e3f-4c94-b6b1-a88e8718d0a6"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 8, 58, 23, 429, DateTimeKind.Local).AddTicks(8586), new DateTime(2025, 11, 23, 8, 58, 23, 429, DateTimeKind.Local).AddTicks(8565) });
        }
    }
}
