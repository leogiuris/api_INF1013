using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModelagemAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddTipoAviso : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdTurmaFK",
                table: "Prova");

            migrationBuilder.AddColumn<int>(
                name: "tipoAviso",
                table: "Aviso",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tipoAviso",
                table: "Aviso");

            migrationBuilder.AddColumn<int>(
                name: "IdTurmaFK",
                table: "Prova",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
