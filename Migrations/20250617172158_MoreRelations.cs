using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModelagemAPI.Migrations
{
    /// <inheritdoc />
    public partial class MoreRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Numero",
                table: "Sala",
                newName: "numero");

            migrationBuilder.CreateTable(
                name: "Prova",
                columns: table => new
                {
                    idProva = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    dataHora = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    codDisciplina = table.Column<int>(type: "int", nullable: false),
                    tipo = table.Column<string>(type: "varchar(50)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prova", x => x.idProva);
                    table.ForeignKey(
                        name: "FK_Prova_Disciplina_codDisciplina",
                        column: x => x.codDisciplina,
                        principalTable: "Disciplina",
                        principalColumn: "codDisciplina",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prova_TipoProva_tipo",
                        column: x => x.tipo,
                        principalTable: "TipoProva",
                        principalColumn: "tipo",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Aviso",
                columns: table => new
                {
                    idAviso = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    idAluno = table.Column<int>(type: "int", nullable: false),
                    idProva = table.Column<int>(type: "int", nullable: false),
                    dataEnviado = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    dataAEnviar = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    mensagem = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    idTurma = table.Column<int>(type: "int", nullable: false),
                    codDisciplina = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aviso", x => x.idAviso);
                    table.ForeignKey(
                        name: "FK_Aviso_Aluno_idAluno",
                        column: x => x.idAluno,
                        principalTable: "Aluno",
                        principalColumn: "idAluno",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Aviso_Disciplina_codDisciplina",
                        column: x => x.codDisciplina,
                        principalTable: "Disciplina",
                        principalColumn: "codDisciplina",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Aviso_Prova_idProva",
                        column: x => x.idProva,
                        principalTable: "Prova",
                        principalColumn: "idProva",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Aviso_Turma_idTurma",
                        column: x => x.idTurma,
                        principalTable: "Turma",
                        principalColumn: "idTurma",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Aviso_codDisciplina",
                table: "Aviso",
                column: "codDisciplina");

            migrationBuilder.CreateIndex(
                name: "IX_Aviso_idAluno",
                table: "Aviso",
                column: "idAluno");

            migrationBuilder.CreateIndex(
                name: "IX_Aviso_idProva",
                table: "Aviso",
                column: "idProva");

            migrationBuilder.CreateIndex(
                name: "IX_Aviso_idTurma",
                table: "Aviso",
                column: "idTurma");

            migrationBuilder.CreateIndex(
                name: "IX_Prova_codDisciplina",
                table: "Prova",
                column: "codDisciplina");

            migrationBuilder.CreateIndex(
                name: "IX_Prova_tipo",
                table: "Prova",
                column: "tipo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aviso");

            migrationBuilder.DropTable(
                name: "Prova");

            migrationBuilder.RenameColumn(
                name: "numero",
                table: "Sala",
                newName: "Numero");
        }
    }
}
