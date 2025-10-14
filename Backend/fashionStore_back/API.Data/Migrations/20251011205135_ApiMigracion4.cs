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
            migrationBuilder.CreateTable(
                name: "ProductosFotos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductoId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    EsPrincipal = table.Column<bool>(type: "bit", nullable: false),
                    Orden = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaActualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductosFotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductosFotos_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ProductosFotos_Id",
                table: "ProductosFotos",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductosFotos_ProductoId",
                table: "ProductosFotos",
                column: "ProductoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductosFotos");

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 11, 15, 2, 32, 483, DateTimeKind.Local).AddTicks(7937), new DateTime(2025, 10, 11, 15, 2, 32, 483, DateTimeKind.Local).AddTicks(7936) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 11, 15, 2, 32, 483, DateTimeKind.Local).AddTicks(7929), new DateTime(2025, 10, 11, 15, 2, 32, 483, DateTimeKind.Local).AddTicks(7926) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 11, 15, 2, 32, 483, DateTimeKind.Local).AddTicks(7970), new DateTime(2025, 10, 11, 15, 2, 32, 483, DateTimeKind.Local).AddTicks(7956) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 11, 15, 2, 32, 483, DateTimeKind.Local).AddTicks(7977), new DateTime(2025, 10, 11, 15, 2, 32, 483, DateTimeKind.Local).AddTicks(7975) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 11, 15, 2, 32, 483, DateTimeKind.Local).AddTicks(7952), new DateTime(2025, 10, 11, 15, 2, 32, 483, DateTimeKind.Local).AddTicks(7951) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 11, 15, 2, 32, 483, DateTimeKind.Local).AddTicks(7946), new DateTime(2025, 10, 11, 15, 2, 32, 483, DateTimeKind.Local).AddTicks(7944) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 11, 15, 2, 32, 483, DateTimeKind.Local).AddTicks(8289), new DateTime(2025, 10, 11, 15, 2, 32, 483, DateTimeKind.Local).AddTicks(8287) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 11, 15, 2, 32, 483, DateTimeKind.Local).AddTicks(8280), new DateTime(2025, 10, 11, 15, 2, 32, 483, DateTimeKind.Local).AddTicks(8275) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 11, 15, 2, 32, 483, DateTimeKind.Local).AddTicks(8304), new DateTime(2025, 10, 11, 15, 2, 32, 483, DateTimeKind.Local).AddTicks(8302) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 11, 15, 2, 32, 483, DateTimeKind.Local).AddTicks(8312), new DateTime(2025, 10, 11, 15, 2, 32, 483, DateTimeKind.Local).AddTicks(8308) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 11, 15, 2, 32, 483, DateTimeKind.Local).AddTicks(8299), new DateTime(2025, 10, 11, 15, 2, 32, 483, DateTimeKind.Local).AddTicks(8297) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 11, 15, 2, 32, 483, DateTimeKind.Local).AddTicks(8294), new DateTime(2025, 10, 11, 15, 2, 32, 483, DateTimeKind.Local).AddTicks(8292) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336522"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 11, 15, 2, 32, 483, DateTimeKind.Local).AddTicks(7816), new DateTime(2025, 10, 11, 15, 2, 32, 483, DateTimeKind.Local).AddTicks(7758) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336523"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 11, 15, 2, 32, 483, DateTimeKind.Local).AddTicks(7844), new DateTime(2025, 10, 11, 15, 2, 32, 483, DateTimeKind.Local).AddTicks(7842) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336524"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 11, 15, 2, 32, 483, DateTimeKind.Local).AddTicks(7854), new DateTime(2025, 10, 11, 15, 2, 32, 483, DateTimeKind.Local).AddTicks(7848) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("42717fb8-6e3f-4c94-b6b1-a88e8718d0a6"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 11, 15, 2, 32, 484, DateTimeKind.Local).AddTicks(7558), new DateTime(2025, 10, 11, 15, 2, 32, 484, DateTimeKind.Local).AddTicks(7513) });
        }
    }
}
