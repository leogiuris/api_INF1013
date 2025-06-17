using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModelagemAPI.Migrations
{
    /// <inheritdoc />
    public partial class outrasJoinTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DisciplinacodDisciplina",
                table: "Turma",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "idSala",
                table: "Prova",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "idTurma",
                table: "Prova",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TurmaSala",
                columns: table => new
                {
                    salasidSala = table.Column<int>(type: "int", nullable: false),
                    turmasidTurma = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurmaSala", x => new { x.salasidSala, x.turmasidTurma });
                    table.ForeignKey(
                        name: "FK_TurmaSala_Sala_salasidSala",
                        column: x => x.salasidSala,
                        principalTable: "Sala",
                        principalColumn: "idSala",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TurmaSala_Turma_turmasidTurma",
                        column: x => x.turmasidTurma,
                        principalTable: "Turma",
                        principalColumn: "idTurma",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Turma_DisciplinacodDisciplina",
                table: "Turma",
                column: "DisciplinacodDisciplina");

            migrationBuilder.CreateIndex(
                name: "IX_Prova_idSala",
                table: "Prova",
                column: "idSala");

            migrationBuilder.CreateIndex(
                name: "IX_Prova_idTurma",
                table: "Prova",
                column: "idTurma");

            migrationBuilder.CreateIndex(
                name: "IX_TurmaSala_turmasidTurma",
                table: "TurmaSala",
                column: "turmasidTurma");

            migrationBuilder.AddForeignKey(
                name: "FK_Prova_Sala_idSala",
                table: "Prova",
                column: "idSala",
                principalTable: "Sala",
                principalColumn: "idSala",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prova_Turma_idTurma",
                table: "Prova",
                column: "idTurma",
                principalTable: "Turma",
                principalColumn: "idTurma",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Turma_Disciplina_DisciplinacodDisciplina",
                table: "Turma",
                column: "DisciplinacodDisciplina",
                principalTable: "Disciplina",
                principalColumn: "codDisciplina");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prova_Sala_idSala",
                table: "Prova");

            migrationBuilder.DropForeignKey(
                name: "FK_Prova_Turma_idTurma",
                table: "Prova");

            migrationBuilder.DropForeignKey(
                name: "FK_Turma_Disciplina_DisciplinacodDisciplina",
                table: "Turma");

            migrationBuilder.DropTable(
                name: "TurmaSala");

            migrationBuilder.DropIndex(
                name: "IX_Turma_DisciplinacodDisciplina",
                table: "Turma");

            migrationBuilder.DropIndex(
                name: "IX_Prova_idSala",
                table: "Prova");

            migrationBuilder.DropIndex(
                name: "IX_Prova_idTurma",
                table: "Prova");

            migrationBuilder.DropColumn(
                name: "DisciplinacodDisciplina",
                table: "Turma");

            migrationBuilder.DropColumn(
                name: "idSala",
                table: "Prova");

            migrationBuilder.DropColumn(
                name: "idTurma",
                table: "Prova");
        }
    }
}
