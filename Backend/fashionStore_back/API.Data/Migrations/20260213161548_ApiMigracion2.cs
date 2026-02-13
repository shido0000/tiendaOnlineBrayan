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
            migrationBuilder.DropColumn(
                name: "BotonTexto",
                table: "BannerPromocions");

            migrationBuilder.DropColumn(
                name: "BotonVinculo",
                table: "BannerPromocions");

            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "BannerPromocions");

            migrationBuilder.DropColumn(
                name: "Destacado",
                table: "BannerPromocions");

            migrationBuilder.DropColumn(
                name: "Dispositivos",
                table: "BannerPromocions");

            migrationBuilder.DropColumn(
                name: "FechaFin",
                table: "BannerPromocions");

            migrationBuilder.DropColumn(
                name: "FechaInicio",
                table: "BannerPromocions");

            migrationBuilder.DropColumn(
                name: "Orden",
                table: "BannerPromocions");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "BannerPromocions",
                newName: "TextoBoton");

            migrationBuilder.AlterColumn<string>(
                name: "TextoTitulo",
                table: "BannerPromocions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TextoSubtitulo",
                table: "BannerPromocions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "CuentasContables",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 11, 15, 47, 867, DateTimeKind.Local).AddTicks(8117), new DateTime(2026, 2, 13, 11, 15, 47, 867, DateTimeKind.Local).AddTicks(8044) });

            migrationBuilder.UpdateData(
                table: "CuentasContables",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 11, 15, 47, 867, DateTimeKind.Local).AddTicks(8167), new DateTime(2026, 2, 13, 11, 15, 47, 867, DateTimeKind.Local).AddTicks(8163) });

            migrationBuilder.UpdateData(
                table: "CuentasContables",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 11, 15, 47, 867, DateTimeKind.Local).AddTicks(8175), new DateTime(2026, 2, 13, 11, 15, 47, 867, DateTimeKind.Local).AddTicks(8173) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6408), new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6406) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6400), new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6398) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6507), new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6505) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6515), new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6513) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6417), new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6416) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6412), new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6411) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6755), new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6753) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6746), new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6743) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6767), new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6766) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6772), new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6770) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6763), new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6762) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6759), new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6758) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336522"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6300), new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6245) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336523"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6331), new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6329) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336524"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6337), new DateTime(2026, 2, 13, 11, 15, 47, 855, DateTimeKind.Local).AddTicks(6335) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("42717fb8-6e3f-4c94-b6b1-a88e8718d0a6"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 2, 13, 11, 15, 47, 856, DateTimeKind.Local).AddTicks(3275), new DateTime(2026, 2, 13, 11, 15, 47, 856, DateTimeKind.Local).AddTicks(3255) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TextoBoton",
                table: "BannerPromocions",
                newName: "Nombre");

            migrationBuilder.AlterColumn<string>(
                name: "TextoTitulo",
                table: "BannerPromocions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "TextoSubtitulo",
                table: "BannerPromocions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "BotonTexto",
                table: "BannerPromocions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BotonVinculo",
                table: "BannerPromocions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "BannerPromocions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Destacado",
                table: "BannerPromocions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Dispositivos",
                table: "BannerPromocions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaFin",
                table: "BannerPromocions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaInicio",
                table: "BannerPromocions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Orden",
                table: "BannerPromocions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "CuentasContables",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 1, 23, 16, 43, 48, 439, DateTimeKind.Local).AddTicks(957), new DateTime(2026, 1, 23, 16, 43, 48, 439, DateTimeKind.Local).AddTicks(900) });

            migrationBuilder.UpdateData(
                table: "CuentasContables",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 1, 23, 16, 43, 48, 439, DateTimeKind.Local).AddTicks(984), new DateTime(2026, 1, 23, 16, 43, 48, 439, DateTimeKind.Local).AddTicks(982) });

            migrationBuilder.UpdateData(
                table: "CuentasContables",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 1, 23, 16, 43, 48, 439, DateTimeKind.Local).AddTicks(990), new DateTime(2026, 1, 23, 16, 43, 48, 439, DateTimeKind.Local).AddTicks(989) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 1, 23, 16, 43, 48, 427, DateTimeKind.Local).AddTicks(7842), new DateTime(2026, 1, 23, 16, 43, 48, 427, DateTimeKind.Local).AddTicks(7841) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 1, 23, 16, 43, 48, 427, DateTimeKind.Local).AddTicks(7830), new DateTime(2026, 1, 23, 16, 43, 48, 427, DateTimeKind.Local).AddTicks(7829) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 1, 23, 16, 43, 48, 427, DateTimeKind.Local).AddTicks(7858), new DateTime(2026, 1, 23, 16, 43, 48, 427, DateTimeKind.Local).AddTicks(7857) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 1, 23, 16, 43, 48, 427, DateTimeKind.Local).AddTicks(7865), new DateTime(2026, 1, 23, 16, 43, 48, 427, DateTimeKind.Local).AddTicks(7864) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 1, 23, 16, 43, 48, 427, DateTimeKind.Local).AddTicks(7852), new DateTime(2026, 1, 23, 16, 43, 48, 427, DateTimeKind.Local).AddTicks(7851) });

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 1, 23, 16, 43, 48, 427, DateTimeKind.Local).AddTicks(7847), new DateTime(2026, 1, 23, 16, 43, 48, 427, DateTimeKind.Local).AddTicks(7845) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("4129cf49-cc22-46a1-9625-501855f2da8b"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 1, 23, 16, 43, 48, 427, DateTimeKind.Local).AddTicks(8114), new DateTime(2026, 1, 23, 16, 43, 48, 427, DateTimeKind.Local).AddTicks(8113) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("56b3924b-209b-40fb-9f31-ad75c12f4528"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 1, 23, 16, 43, 48, 427, DateTimeKind.Local).AddTicks(8105), new DateTime(2026, 1, 23, 16, 43, 48, 427, DateTimeKind.Local).AddTicks(8102) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a301"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 1, 23, 16, 43, 48, 427, DateTimeKind.Local).AddTicks(8128), new DateTime(2026, 1, 23, 16, 43, 48, 427, DateTimeKind.Local).AddTicks(8126) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("80abf232-a641-478d-8720-f0ae49e8a302"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 1, 23, 16, 43, 48, 427, DateTimeKind.Local).AddTicks(8132), new DateTime(2026, 1, 23, 16, 43, 48, 427, DateTimeKind.Local).AddTicks(8131) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("90abf232-a641-478d-8720-f0ae49e8a306"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 1, 23, 16, 43, 48, 427, DateTimeKind.Local).AddTicks(8123), new DateTime(2026, 1, 23, 16, 43, 48, 427, DateTimeKind.Local).AddTicks(8122) });

            migrationBuilder.UpdateData(
                table: "RolPermiso",
                keyColumn: "Id",
                keyValue: new Guid("e36d283c-8b25-42b6-83bd-56edd953e770"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 1, 23, 16, 43, 48, 427, DateTimeKind.Local).AddTicks(8119), new DateTime(2026, 1, 23, 16, 43, 48, 427, DateTimeKind.Local).AddTicks(8118) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336522"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 1, 23, 16, 43, 48, 427, DateTimeKind.Local).AddTicks(7729), new DateTime(2026, 1, 23, 16, 43, 48, 427, DateTimeKind.Local).AddTicks(7673) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336523"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 1, 23, 16, 43, 48, 427, DateTimeKind.Local).AddTicks(7755), new DateTime(2026, 1, 23, 16, 43, 48, 427, DateTimeKind.Local).AddTicks(7753) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c0b7e3b3-a06e-4580-b985-bb2fc4336524"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 1, 23, 16, 43, 48, 427, DateTimeKind.Local).AddTicks(7761), new DateTime(2026, 1, 23, 16, 43, 48, 427, DateTimeKind.Local).AddTicks(7759) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("42717fb8-6e3f-4c94-b6b1-a88e8718d0a6"),
                columns: new[] { "FechaActualizado", "FechaCreado" },
                values: new object[] { new DateTime(2026, 1, 23, 16, 43, 48, 428, DateTimeKind.Local).AddTicks(4653), new DateTime(2026, 1, 23, 16, 43, 48, 428, DateTimeKind.Local).AddTicks(4627) });
        }
    }
}
