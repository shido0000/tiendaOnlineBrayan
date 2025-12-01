using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class ApiMigracion8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidosDetalles_Productos_ProductoId",
                table: "PedidosDetalles");

            migrationBuilder.RenameColumn(
                name: "ProductoId",
                table: "PedidosDetalles",
                newName: "ProductoVarianteId");

            migrationBuilder.RenameIndex(
                name: "IX_PedidosDetalles_ProductoId",
                table: "PedidosDetalles",
                newName: "IX_PedidosDetalles_ProductoVarianteId");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductoVarianteId1",
                table: "PedidosDetalles",
                type: "uniqueidentifier",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_PedidosDetalles_ProductoVarianteId1",
                table: "PedidosDetalles",
                column: "ProductoVarianteId1");

            migrationBuilder.AddForeignKey(
                name: "FK_PedidosDetalles_ProductoVariantes_ProductoVarianteId",
                table: "PedidosDetalles",
                column: "ProductoVarianteId",
                principalTable: "ProductoVariantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PedidosDetalles_ProductoVariantes_ProductoVarianteId1",
                table: "PedidosDetalles",
                column: "ProductoVarianteId1",
                principalTable: "ProductoVariantes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidosDetalles_ProductoVariantes_ProductoVarianteId",
                table: "PedidosDetalles");

            migrationBuilder.DropForeignKey(
                name: "FK_PedidosDetalles_ProductoVariantes_ProductoVarianteId1",
                table: "PedidosDetalles");

            migrationBuilder.DropIndex(
                name: "IX_PedidosDetalles_ProductoVarianteId1",
                table: "PedidosDetalles");

            migrationBuilder.DropColumn(
                name: "ProductoVarianteId1",
                table: "PedidosDetalles");

            migrationBuilder.RenameColumn(
                name: "ProductoVarianteId",
                table: "PedidosDetalles",
                newName: "ProductoId");

            migrationBuilder.RenameIndex(
                name: "IX_PedidosDetalles_ProductoVarianteId",
                table: "PedidosDetalles",
                newName: "IX_PedidosDetalles_ProductoId");

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 22, 5, 43, 536, DateTimeKind.Local).AddTicks(3858), new DateTime(2025, 11, 23, 22, 5, 43, 536, DateTimeKind.Local).AddTicks(3854) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 22, 5, 43, 536, DateTimeKind.Local).AddTicks(3836), new DateTime(2025, 11, 23, 22, 5, 43, 536, DateTimeKind.Local).AddTicks(3829) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 22, 5, 43, 536, DateTimeKind.Local).AddTicks(3914), new DateTime(2025, 11, 23, 22, 5, 43, 536, DateTimeKind.Local).AddTicks(3893) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 22, 5, 43, 536, DateTimeKind.Local).AddTicks(3931), new DateTime(2025, 11, 23, 22, 5, 43, 536, DateTimeKind.Local).AddTicks(3927) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 22, 5, 43, 536, DateTimeKind.Local).AddTicks(3886), new DateTime(2025, 11, 23, 22, 5, 43, 536, DateTimeKind.Local).AddTicks(3883) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 22, 5, 43, 536, DateTimeKind.Local).AddTicks(3875), new DateTime(2025, 11, 23, 22, 5, 43, 536, DateTimeKind.Local).AddTicks(3871) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 22, 5, 43, 536, DateTimeKind.Local).AddTicks(4377), new DateTime(2025, 11, 23, 22, 5, 43, 536, DateTimeKind.Local).AddTicks(4373) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 22, 5, 43, 536, DateTimeKind.Local).AddTicks(4353), new DateTime(2025, 11, 23, 22, 5, 43, 536, DateTimeKind.Local).AddTicks(4342) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 22, 5, 43, 536, DateTimeKind.Local).AddTicks(4405), new DateTime(2025, 11, 23, 22, 5, 43, 536, DateTimeKind.Local).AddTicks(4401) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 22, 5, 43, 536, DateTimeKind.Local).AddTicks(4426), new DateTime(2025, 11, 23, 22, 5, 43, 536, DateTimeKind.Local).AddTicks(4415) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 22, 5, 43, 536, DateTimeKind.Local).AddTicks(4395), new DateTime(2025, 11, 23, 22, 5, 43, 536, DateTimeKind.Local).AddTicks(4392) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 22, 5, 43, 536, DateTimeKind.Local).AddTicks(4387), new DateTime(2025, 11, 23, 22, 5, 43, 536, DateTimeKind.Local).AddTicks(4384) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336522"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 22, 5, 43, 536, DateTimeKind.Local).AddTicks(3618), new DateTime(2025, 11, 23, 22, 5, 43, 536, DateTimeKind.Local).AddTicks(3326) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336523"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 22, 5, 43, 536, DateTimeKind.Local).AddTicks(3652), new DateTime(2025, 11, 23, 22, 5, 43, 536, DateTimeKind.Local).AddTicks(3649) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336524"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 22, 5, 43, 536, DateTimeKind.Local).AddTicks(3662), new DateTime(2025, 11, 23, 22, 5, 43, 536, DateTimeKind.Local).AddTicks(3659) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("42717fb8-6e3f-4c94-b6b1-a88e8718d0a6"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2025, 11, 23, 22, 5, 43, 538, DateTimeKind.Local).AddTicks(1055), new DateTime(2025, 11, 23, 22, 5, 43, 538, DateTimeKind.Local).AddTicks(991) });

            migrationBuilder.AddForeignKey(
                name: "FK_PedidosDetalles_Productos_ProductoId",
                table: "PedidosDetalles",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
