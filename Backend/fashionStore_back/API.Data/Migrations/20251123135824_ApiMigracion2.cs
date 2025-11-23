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
                name: "Gestores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaActualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gestores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GestorPedidos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GestorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PedidoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaActualizado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GestorPedidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GestorPedidos_Gestores_GestorId",
                        column: x => x.GestorId,
                        principalTable: "Gestores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GestorPedidos_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Gestores_Id",
                table: "Gestores",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GestorPedidos_GestorId",
                table: "GestorPedidos",
                column: "GestorId");

            migrationBuilder.CreateIndex(
                name: "IX_GestorPedidos_Id",
                table: "GestorPedidos",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GestorPedidos_PedidoId",
                table: "GestorPedidos",
                column: "PedidoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GestorPedidos");

            migrationBuilder.DropTable(
                name: "Gestores");

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 22, 20, 1, 15, 747, DateTimeKind.Local).AddTicks(3344), new DateTime(2025, 11, 22, 20, 1, 15, 747, DateTimeKind.Local).AddTicks(3341) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 22, 20, 1, 15, 747, DateTimeKind.Local).AddTicks(3326), new DateTime(2025, 11, 22, 20, 1, 15, 747, DateTimeKind.Local).AddTicks(3321) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 22, 20, 1, 15, 747, DateTimeKind.Local).AddTicks(3499), new DateTime(2025, 11, 22, 20, 1, 15, 747, DateTimeKind.Local).AddTicks(3476) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 22, 20, 1, 15, 747, DateTimeKind.Local).AddTicks(3509), new DateTime(2025, 11, 22, 20, 1, 15, 747, DateTimeKind.Local).AddTicks(3507) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 22, 20, 1, 15, 747, DateTimeKind.Local).AddTicks(3470), new DateTime(2025, 11, 22, 20, 1, 15, 747, DateTimeKind.Local).AddTicks(3468) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 22, 20, 1, 15, 747, DateTimeKind.Local).AddTicks(3462), new DateTime(2025, 11, 22, 20, 1, 15, 747, DateTimeKind.Local).AddTicks(3454) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 22, 20, 1, 15, 747, DateTimeKind.Local).AddTicks(4042), new DateTime(2025, 11, 22, 20, 1, 15, 747, DateTimeKind.Local).AddTicks(4040) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 22, 20, 1, 15, 747, DateTimeKind.Local).AddTicks(4030), new DateTime(2025, 11, 22, 20, 1, 15, 747, DateTimeKind.Local).AddTicks(4021) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 22, 20, 1, 15, 747, DateTimeKind.Local).AddTicks(4060), new DateTime(2025, 11, 22, 20, 1, 15, 747, DateTimeKind.Local).AddTicks(4058) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 22, 20, 1, 15, 747, DateTimeKind.Local).AddTicks(4077), new DateTime(2025, 11, 22, 20, 1, 15, 747, DateTimeKind.Local).AddTicks(4065) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 22, 20, 1, 15, 747, DateTimeKind.Local).AddTicks(4054), new DateTime(2025, 11, 22, 20, 1, 15, 747, DateTimeKind.Local).AddTicks(4052) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 22, 20, 1, 15, 747, DateTimeKind.Local).AddTicks(4048), new DateTime(2025, 11, 22, 20, 1, 15, 747, DateTimeKind.Local).AddTicks(4046) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336522"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 22, 20, 1, 15, 747, DateTimeKind.Local).AddTicks(3149), new DateTime(2025, 11, 22, 20, 1, 15, 747, DateTimeKind.Local).AddTicks(3089) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336523"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 22, 20, 1, 15, 747, DateTimeKind.Local).AddTicks(3178), new DateTime(2025, 11, 22, 20, 1, 15, 747, DateTimeKind.Local).AddTicks(3176) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336524"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 22, 20, 1, 15, 747, DateTimeKind.Local).AddTicks(3185), new DateTime(2025, 11, 22, 20, 1, 15, 747, DateTimeKind.Local).AddTicks(3183) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("42717fb8-6e3f-4c94-b6b1-a88e8718d0a6"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 22, 20, 1, 15, 748, DateTimeKind.Local).AddTicks(6468), new DateTime(2025, 11, 22, 20, 1, 15, 748, DateTimeKind.Local).AddTicks(6425) });
        }
    }
}
