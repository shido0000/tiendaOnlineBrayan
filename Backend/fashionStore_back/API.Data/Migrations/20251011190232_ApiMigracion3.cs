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
            migrationBuilder.DropForeignKey(
                name: "FK_Descuentos_Productos_ProductoId",
                table: "Descuentos");

            migrationBuilder.DropIndex(
                name: "IX_Descuentos_ProductoId",
                table: "Descuentos");

            migrationBuilder.DropColumn(
                name: "ProductoId",
                table: "Descuentos");

            migrationBuilder.CreateTable(
                name: "ProductosDescuentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DescuentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaActualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductosDescuentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductosDescuentos_Descuentos_DescuentoId",
                        column: x => x.DescuentoId,
                        principalTable: "Descuentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductosDescuentos_Productos_ProductoId",
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

            migrationBuilder.CreateIndex(
                name: "IX_ProductosDescuentos_DescuentoId",
                table: "ProductosDescuentos",
                column: "DescuentoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductosDescuentos_Id",
                table: "ProductosDescuentos",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductosDescuentos_ProductoId",
                table: "ProductosDescuentos",
                column: "ProductoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductosDescuentos");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductoId",
                table: "Descuentos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(8667), new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(8664) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(8655), new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(8650) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(8688), new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(8686) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(8699), new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(8696) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(8681), new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(8679) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(8674), new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(8672) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(9118), new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(9116) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(9103), new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(9099) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(9136), new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(9134) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(9145), new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(9142) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(9130), new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(9128) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(9124), new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(9122) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336522"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(8370), new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(8319) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336523"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(8401), new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(8398) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336524"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(8408), new DateTime(2025, 10, 10, 21, 33, 41, 475, DateTimeKind.Local).AddTicks(8406) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("42717fb8-6e3f-4c94-b6b1-a88e8718d0a6"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 10, 10, 21, 33, 41, 477, DateTimeKind.Local).AddTicks(2029), new DateTime(2025, 10, 10, 21, 33, 41, 477, DateTimeKind.Local).AddTicks(1990) });

            migrationBuilder.CreateIndex(
                name: "IX_Descuentos_ProductoId",
                table: "Descuentos",
                column: "ProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Descuentos_Productos_ProductoId",
                table: "Descuentos",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
