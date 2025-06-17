using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModelagemAPI.Migrations
{
    /// <inheritdoc />
    public partial class addedDBSets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aviso_Aluno_idAluno",
                table: "Aviso");

            migrationBuilder.DropForeignKey(
                name: "FK_Aviso_Disciplina_codDisciplina",
                table: "Aviso");

            migrationBuilder.DropForeignKey(
                name: "FK_Aviso_Prova_idProva",
                table: "Aviso");

            migrationBuilder.DropForeignKey(
                name: "FK_Aviso_Turma_idTurma",
                table: "Aviso");

            migrationBuilder.DropForeignKey(
                name: "FK_Prova_Disciplina_codDisciplina",
                table: "Prova");

            migrationBuilder.DropForeignKey(
                name: "FK_Prova_TipoProva_tipo",
                table: "Prova");

            migrationBuilder.DropForeignKey(
                name: "FK_Turma_Disciplina_codDisciplina",
                table: "Turma");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Turma",
                table: "Turma");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TipoProva",
                table: "TipoProva");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sala",
                table: "Sala");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Prova",
                table: "Prova");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Disciplina",
                table: "Disciplina");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Aviso",
                table: "Aviso");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Aluno",
                table: "Aluno");

            migrationBuilder.RenameTable(
                name: "Turma",
                newName: "Turmas");

            migrationBuilder.RenameTable(
                name: "TipoProva",
                newName: "TiposProva");

            migrationBuilder.RenameTable(
                name: "Sala",
                newName: "Salas");

            migrationBuilder.RenameTable(
                name: "Prova",
                newName: "Provas");

            migrationBuilder.RenameTable(
                name: "Disciplina",
                newName: "Disciplinas");

            migrationBuilder.RenameTable(
                name: "Aviso",
                newName: "Avisos");

            migrationBuilder.RenameTable(
                name: "Aluno",
                newName: "Alunos");

            migrationBuilder.RenameIndex(
                name: "IX_Turma_codDisciplina",
                table: "Turmas",
                newName: "IX_Turmas_codDisciplina");

            migrationBuilder.RenameIndex(
                name: "IX_Prova_tipo",
                table: "Provas",
                newName: "IX_Provas_tipo");

            migrationBuilder.RenameIndex(
                name: "IX_Prova_codDisciplina",
                table: "Provas",
                newName: "IX_Provas_codDisciplina");

            migrationBuilder.RenameIndex(
                name: "IX_Aviso_idTurma",
                table: "Avisos",
                newName: "IX_Avisos_idTurma");

            migrationBuilder.RenameIndex(
                name: "IX_Aviso_idProva",
                table: "Avisos",
                newName: "IX_Avisos_idProva");

            migrationBuilder.RenameIndex(
                name: "IX_Aviso_idAluno",
                table: "Avisos",
                newName: "IX_Avisos_idAluno");

            migrationBuilder.RenameIndex(
                name: "IX_Aviso_codDisciplina",
                table: "Avisos",
                newName: "IX_Avisos_codDisciplina");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Turmas",
                table: "Turmas",
                column: "idTurma");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TiposProva",
                table: "TiposProva",
                column: "tipo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Salas",
                table: "Salas",
                column: "idSala");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Provas",
                table: "Provas",
                column: "idProva");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Disciplinas",
                table: "Disciplinas",
                column: "codDisciplina");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Avisos",
                table: "Avisos",
                column: "idAviso");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Alunos",
                table: "Alunos",
                column: "idAluno");

            migrationBuilder.AddForeignKey(
                name: "FK_Avisos_Alunos_idAluno",
                table: "Avisos",
                column: "idAluno",
                principalTable: "Alunos",
                principalColumn: "idAluno",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Avisos_Disciplinas_codDisciplina",
                table: "Avisos",
                column: "codDisciplina",
                principalTable: "Disciplinas",
                principalColumn: "codDisciplina",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Avisos_Provas_idProva",
                table: "Avisos",
                column: "idProva",
                principalTable: "Provas",
                principalColumn: "idProva",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Avisos_Turmas_idTurma",
                table: "Avisos",
                column: "idTurma",
                principalTable: "Turmas",
                principalColumn: "idTurma",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Provas_Disciplinas_codDisciplina",
                table: "Provas",
                column: "codDisciplina",
                principalTable: "Disciplinas",
                principalColumn: "codDisciplina",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Provas_TiposProva_tipo",
                table: "Provas",
                column: "tipo",
                principalTable: "TiposProva",
                principalColumn: "tipo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Turmas_Disciplinas_codDisciplina",
                table: "Turmas",
                column: "codDisciplina",
                principalTable: "Disciplinas",
                principalColumn: "codDisciplina",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avisos_Alunos_idAluno",
                table: "Avisos");

            migrationBuilder.DropForeignKey(
                name: "FK_Avisos_Disciplinas_codDisciplina",
                table: "Avisos");

            migrationBuilder.DropForeignKey(
                name: "FK_Avisos_Provas_idProva",
                table: "Avisos");

            migrationBuilder.DropForeignKey(
                name: "FK_Avisos_Turmas_idTurma",
                table: "Avisos");

            migrationBuilder.DropForeignKey(
                name: "FK_Provas_Disciplinas_codDisciplina",
                table: "Provas");

            migrationBuilder.DropForeignKey(
                name: "FK_Provas_TiposProva_tipo",
                table: "Provas");

            migrationBuilder.DropForeignKey(
                name: "FK_Turmas_Disciplinas_codDisciplina",
                table: "Turmas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Turmas",
                table: "Turmas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TiposProva",
                table: "TiposProva");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Salas",
                table: "Salas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Provas",
                table: "Provas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Disciplinas",
                table: "Disciplinas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Avisos",
                table: "Avisos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Alunos",
                table: "Alunos");

            migrationBuilder.RenameTable(
                name: "Turmas",
                newName: "Turma");

            migrationBuilder.RenameTable(
                name: "TiposProva",
                newName: "TipoProva");

            migrationBuilder.RenameTable(
                name: "Salas",
                newName: "Sala");

            migrationBuilder.RenameTable(
                name: "Provas",
                newName: "Prova");

            migrationBuilder.RenameTable(
                name: "Disciplinas",
                newName: "Disciplina");

            migrationBuilder.RenameTable(
                name: "Avisos",
                newName: "Aviso");

            migrationBuilder.RenameTable(
                name: "Alunos",
                newName: "Aluno");

            migrationBuilder.RenameIndex(
                name: "IX_Turmas_codDisciplina",
                table: "Turma",
                newName: "IX_Turma_codDisciplina");

            migrationBuilder.RenameIndex(
                name: "IX_Provas_tipo",
                table: "Prova",
                newName: "IX_Prova_tipo");

            migrationBuilder.RenameIndex(
                name: "IX_Provas_codDisciplina",
                table: "Prova",
                newName: "IX_Prova_codDisciplina");

            migrationBuilder.RenameIndex(
                name: "IX_Avisos_idTurma",
                table: "Aviso",
                newName: "IX_Aviso_idTurma");

            migrationBuilder.RenameIndex(
                name: "IX_Avisos_idProva",
                table: "Aviso",
                newName: "IX_Aviso_idProva");

            migrationBuilder.RenameIndex(
                name: "IX_Avisos_idAluno",
                table: "Aviso",
                newName: "IX_Aviso_idAluno");

            migrationBuilder.RenameIndex(
                name: "IX_Avisos_codDisciplina",
                table: "Aviso",
                newName: "IX_Aviso_codDisciplina");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Turma",
                table: "Turma",
                column: "idTurma");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TipoProva",
                table: "TipoProva",
                column: "tipo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sala",
                table: "Sala",
                column: "idSala");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prova",
                table: "Prova",
                column: "idProva");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Disciplina",
                table: "Disciplina",
                column: "codDisciplina");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Aviso",
                table: "Aviso",
                column: "idAviso");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Aluno",
                table: "Aluno",
                column: "idAluno");

            migrationBuilder.AddForeignKey(
                name: "FK_Aviso_Aluno_idAluno",
                table: "Aviso",
                column: "idAluno",
                principalTable: "Aluno",
                principalColumn: "idAluno",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Aviso_Disciplina_codDisciplina",
                table: "Aviso",
                column: "codDisciplina",
                principalTable: "Disciplina",
                principalColumn: "codDisciplina",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Aviso_Prova_idProva",
                table: "Aviso",
                column: "idProva",
                principalTable: "Prova",
                principalColumn: "idProva",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Aviso_Turma_idTurma",
                table: "Aviso",
                column: "idTurma",
                principalTable: "Turma",
                principalColumn: "idTurma",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prova_Disciplina_codDisciplina",
                table: "Prova",
                column: "codDisciplina",
                principalTable: "Disciplina",
                principalColumn: "codDisciplina",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prova_TipoProva_tipo",
                table: "Prova",
                column: "tipo",
                principalTable: "TipoProva",
                principalColumn: "tipo",
                onDelete: ReferentialAction.Cascade);

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
