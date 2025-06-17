using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModelagemAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Aluno",
                columns: table => new
                {
                    idAluno = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aluno", x => x.idAluno);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Disciplina",
                columns: table => new
                {
                    codDisciplina = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplina", x => x.codDisciplina);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Entity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entity", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Sala",
                columns: table => new
                {
                    idSala = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    bloco = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    numero = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sala", x => x.idSala);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TipoProva",
                columns: table => new
                {
                    tipo = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoProva", x => x.tipo);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Turma",
                columns: table => new
                {
                    idTurma = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nomeTurma = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    codDisciplina = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turma", x => x.idTurma);
                    table.ForeignKey(
                        name: "FK_Turma_Disciplina_codDisciplina",
                        column: x => x.codDisciplina,
                        principalTable: "Disciplina",
                        principalColumn: "codDisciplina",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Prova",
                columns: table => new
                {
                    idProva = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    dataHora = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    codDisciplina = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
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
                    codDisciplina = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
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

            migrationBuilder.CreateIndex(
                name: "IX_Turma_codDisciplina",
                table: "Turma",
                column: "codDisciplina");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aviso");

            migrationBuilder.DropTable(
                name: "Entity");

            migrationBuilder.DropTable(
                name: "Sala");

            migrationBuilder.DropTable(
                name: "Aluno");

            migrationBuilder.DropTable(
                name: "Prova");

            migrationBuilder.DropTable(
                name: "Turma");

            migrationBuilder.DropTable(
                name: "TipoProva");

            migrationBuilder.DropTable(
                name: "Disciplina");
        }
    }
}
