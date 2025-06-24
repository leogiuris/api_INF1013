using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModelagemAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddIdTurmaFKToProva : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdTurmaFK",
                table: "Prova",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdTurmaFK",
                table: "Prova");
        }
    }
}
