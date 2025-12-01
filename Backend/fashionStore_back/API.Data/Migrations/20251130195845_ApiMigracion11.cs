using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class ApiMigracion11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OtrasVariantes_ProductoVariantes_ProductoVarianteId",
                table: "OtrasVariantes");

            migrationBuilder.DropIndex(
                name: "IX_OtrasVariantes_ProductoVarianteId",
                table: "OtrasVariantes");

            migrationBuilder.DropColumn(
                name: "ProductoVarianteId",
                table: "OtrasVariantes");

            migrationBuilder.CreateTable(
                name: "OtraVarianteProductoVariantes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OtraVarianteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductoVarianteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaActualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtraVarianteProductoVariantes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OtraVarianteProductoVariantes_OtrasVariantes_OtraVarianteId",
                        column: x => x.OtraVarianteId,
                        principalTable: "OtrasVariantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OtraVarianteProductoVariantes_ProductoVariantes_ProductoVarianteId",
                        column: x => x.ProductoVarianteId,
                        principalTable: "ProductoVariantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 30, 14, 58, 44, 828, DateTimeKind.Local).AddTicks(1308), new DateTime(2025, 11, 30, 14, 58, 44, 828, DateTimeKind.Local).AddTicks(1303) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 30, 14, 58, 44, 828, DateTimeKind.Local).AddTicks(1280), new DateTime(2025, 11, 30, 14, 58, 44, 828, DateTimeKind.Local).AddTicks(1272) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 30, 14, 58, 44, 828, DateTimeKind.Local).AddTicks(1385), new DateTime(2025, 11, 30, 14, 58, 44, 828, DateTimeKind.Local).AddTicks(1345) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 30, 14, 58, 44, 828, DateTimeKind.Local).AddTicks(1404), new DateTime(2025, 11, 30, 14, 58, 44, 828, DateTimeKind.Local).AddTicks(1400) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 30, 14, 58, 44, 828, DateTimeKind.Local).AddTicks(1337), new DateTime(2025, 11, 30, 14, 58, 44, 828, DateTimeKind.Local).AddTicks(1333) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 30, 14, 58, 44, 828, DateTimeKind.Local).AddTicks(1326), new DateTime(2025, 11, 30, 14, 58, 44, 828, DateTimeKind.Local).AddTicks(1321) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 30, 14, 58, 44, 828, DateTimeKind.Local).AddTicks(2000), new DateTime(2025, 11, 30, 14, 58, 44, 828, DateTimeKind.Local).AddTicks(1995) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 30, 14, 58, 44, 828, DateTimeKind.Local).AddTicks(1980), new DateTime(2025, 11, 30, 14, 58, 44, 828, DateTimeKind.Local).AddTicks(1968) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 30, 14, 58, 44, 828, DateTimeKind.Local).AddTicks(2035), new DateTime(2025, 11, 30, 14, 58, 44, 828, DateTimeKind.Local).AddTicks(2028) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 30, 14, 58, 44, 828, DateTimeKind.Local).AddTicks(2065), new DateTime(2025, 11, 30, 14, 58, 44, 828, DateTimeKind.Local).AddTicks(2043) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 30, 14, 58, 44, 828, DateTimeKind.Local).AddTicks(2021), new DateTime(2025, 11, 30, 14, 58, 44, 828, DateTimeKind.Local).AddTicks(2017) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 30, 14, 58, 44, 828, DateTimeKind.Local).AddTicks(2011), new DateTime(2025, 11, 30, 14, 58, 44, 828, DateTimeKind.Local).AddTicks(2007) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336522"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 30, 14, 58, 44, 828, DateTimeKind.Local).AddTicks(1046), new DateTime(2025, 11, 30, 14, 58, 44, 828, DateTimeKind.Local).AddTicks(883) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336523"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 30, 14, 58, 44, 828, DateTimeKind.Local).AddTicks(1090), new DateTime(2025, 11, 30, 14, 58, 44, 828, DateTimeKind.Local).AddTicks(1085) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336524"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 30, 14, 58, 44, 828, DateTimeKind.Local).AddTicks(1104), new DateTime(2025, 11, 30, 14, 58, 44, 828, DateTimeKind.Local).AddTicks(1099) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("42717fb8-6e3f-4c94-b6b1-a88e8718d0a6"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 30, 14, 58, 44, 830, DateTimeKind.Local).AddTicks(4602), new DateTime(2025, 11, 30, 14, 58, 44, 830, DateTimeKind.Local).AddTicks(4548) });

            migrationBuilder.CreateIndex(
                name: "IX_OtraVarianteProductoVariantes_Id",
                table: "OtraVarianteProductoVariantes",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OtraVarianteProductoVariantes_OtraVarianteId",
                table: "OtraVarianteProductoVariantes",
                column: "OtraVarianteId");

            migrationBuilder.CreateIndex(
                name: "IX_OtraVarianteProductoVariantes_ProductoVarianteId",
                table: "OtraVarianteProductoVariantes",
                column: "ProductoVarianteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OtraVarianteProductoVariantes");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductoVarianteId",
                table: "OtrasVariantes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3199), new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3197) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3190), new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3188) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3215), new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3214) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3224), new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3222) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3210), new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3208) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3204), new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3202) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3498), new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3497) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3486), new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3483) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3513), new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3511) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3518), new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3517) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3509), new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3507) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3503), new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3502) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336522"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3077), new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3028) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336523"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3102), new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3100) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336524"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3107), new DateTime(2025, 11, 30, 9, 28, 6, 156, DateTimeKind.Local).AddTicks(3106) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("42717fb8-6e3f-4c94-b6b1-a88e8718d0a6"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 30, 9, 28, 6, 157, DateTimeKind.Local).AddTicks(2953), new DateTime(2025, 11, 30, 9, 28, 6, 157, DateTimeKind.Local).AddTicks(2936) });

            migrationBuilder.CreateIndex(
                name: "IX_OtrasVariantes_ProductoVarianteId",
                table: "OtrasVariantes",
                column: "ProductoVarianteId");

            migrationBuilder.AddForeignKey(
                name: "FK_OtrasVariantes_ProductoVariantes_ProductoVarianteId",
                table: "OtrasVariantes",
                column: "ProductoVarianteId",
                principalTable: "ProductoVariantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
