using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModelagemAPI.Migrations
{
    /// <inheritdoc />
    public partial class jointableAlunoTurma : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlunoTurma",
                columns: table => new
                {
                    alunosidAluno = table.Column<int>(type: "int", nullable: false),
                    turmasidTurma = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunoTurma", x => new { x.alunosidAluno, x.turmasidTurma });
                    table.ForeignKey(
                        name: "FK_AlunoTurma_Aluno_alunosidAluno",
                        column: x => x.alunosidAluno,
                        principalTable: "Aluno",
                        principalColumn: "idAluno",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlunoTurma_Turma_turmasidTurma",
                        column: x => x.turmasidTurma,
                        principalTable: "Turma",
                        principalColumn: "idTurma",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AlunoTurma_turmasidTurma",
                table: "AlunoTurma",
                column: "turmasidTurma");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlunoTurma");
        }
    }
}
