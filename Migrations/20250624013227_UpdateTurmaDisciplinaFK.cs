using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModelagemAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTurmaDisciplinaFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Turma_Disciplina_DisciplinacodDisciplina",
                table: "Turma");

            migrationBuilder.DropForeignKey(
                name: "FK_Turma_Disciplina_codDisciplina",
                table: "Turma");

            migrationBuilder.DropIndex(
                name: "IX_Turma_codDisciplina",
                table: "Turma");

            migrationBuilder.DropIndex(
                name: "IX_Turma_DisciplinacodDisciplina",
                table: "Turma");

            migrationBuilder.DropColumn(
                name: "DisciplinacodDisciplina",
                table: "Turma");

            migrationBuilder.DropColumn(
                name: "codDisciplina",
                table: "Turma");

            migrationBuilder.AddColumn<string>(
                name: "CodDisciplinaFK",
                table: "Turma",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Turma_CodDisciplinaFK",
                table: "Turma",
                column: "CodDisciplinaFK");

            migrationBuilder.AddForeignKey(
                name: "FK_Turma_Disciplina_CodDisciplinaFK",
                table: "Turma",
                column: "CodDisciplinaFK",
                principalTable: "Disciplina",
                principalColumn: "codDisciplina",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Turma_Disciplina_CodDisciplinaFK",
                table: "Turma");

            migrationBuilder.DropIndex(
                name: "IX_Turma_CodDisciplinaFK",
                table: "Turma");

            migrationBuilder.DropColumn(
                name: "CodDisciplinaFK",
                table: "Turma");

            migrationBuilder.AddColumn<string>(
                name: "DisciplinacodDisciplina",
                table: "Turma",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "codDisciplina",
                table: "Turma",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Turma_codDisciplina",
                table: "Turma",
                column: "codDisciplina");

            migrationBuilder.CreateIndex(
                name: "IX_Turma_DisciplinacodDisciplina",
                table: "Turma",
                column: "DisciplinacodDisciplina");

            migrationBuilder.AddForeignKey(
                name: "FK_Turma_Disciplina_DisciplinacodDisciplina",
                table: "Turma",
                column: "DisciplinacodDisciplina",
                principalTable: "Disciplina",
                principalColumn: "codDisciplina");

            migrationBuilder.AddForeignKey(
                name: "FK_Turma_Disciplina_codDisciplina",
                table: "Turma",
                column: "codDisciplina",
                principalTable: "Disciplina",
                principalColumn: "codDisciplina",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
