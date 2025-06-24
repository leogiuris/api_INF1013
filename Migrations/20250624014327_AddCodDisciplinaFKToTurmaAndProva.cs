using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModelagemAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddCodDisciplinaFKToTurmaAndProva : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prova_Disciplina_codDisciplina",
                table: "Prova");

            migrationBuilder.DropIndex(
                name: "IX_Prova_codDisciplina",
                table: "Prova");

            migrationBuilder.DropColumn(
                name: "codDisciplina",
                table: "Prova");

            migrationBuilder.AddColumn<string>(
                name: "CodDisciplinaFK",
                table: "Prova",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Prova_CodDisciplinaFK",
                table: "Prova",
                column: "CodDisciplinaFK");

            migrationBuilder.AddForeignKey(
                name: "FK_Prova_Disciplina_CodDisciplinaFK",
                table: "Prova",
                column: "CodDisciplinaFK",
                principalTable: "Disciplina",
                principalColumn: "codDisciplina",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prova_Disciplina_CodDisciplinaFK",
                table: "Prova");

            migrationBuilder.DropIndex(
                name: "IX_Prova_CodDisciplinaFK",
                table: "Prova");

            migrationBuilder.DropColumn(
                name: "CodDisciplinaFK",
                table: "Prova");

            migrationBuilder.AddColumn<string>(
                name: "codDisciplina",
                table: "Prova",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Prova_codDisciplina",
                table: "Prova",
                column: "codDisciplina");

            migrationBuilder.AddForeignKey(
                name: "FK_Prova_Disciplina_codDisciplina",
                table: "Prova",
                column: "codDisciplina",
                principalTable: "Disciplina",
                principalColumn: "codDisciplina",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
