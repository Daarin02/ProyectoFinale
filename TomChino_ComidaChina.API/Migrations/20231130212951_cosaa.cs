using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TomChino_ComidaChina.API.Migrations
{
    /// <inheritdoc />
    public partial class cosaa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "registrosRestaurantes");

            migrationBuilder.DropColumn(
                name: "Paquete1",
                table: "registrosRestaurantes");

            migrationBuilder.DropColumn(
                name: "Paquete2",
                table: "registrosRestaurantes");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "registrosRestaurantes",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Paquete3",
                table: "registrosRestaurantes",
                newName: "user");

            migrationBuilder.AddColumn<int>(
                name: "Total",
                table: "registrosRestaurantes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total",
                table: "registrosRestaurantes");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "registrosRestaurantes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user",
                table: "registrosRestaurantes",
                newName: "Paquete3");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "registrosRestaurantes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Paquete1",
                table: "registrosRestaurantes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Paquete2",
                table: "registrosRestaurantes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
