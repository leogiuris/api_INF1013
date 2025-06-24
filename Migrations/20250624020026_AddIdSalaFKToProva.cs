using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModelagemAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddIdSalaFKToProva : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prova_Sala_idSala",
                table: "Prova");

            migrationBuilder.DropIndex(
                name: "IX_Prova_idSala",
                table: "Prova");

            migrationBuilder.AddColumn<int>(
                name: "IdSalaFK",
                table: "Prova",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SalaidSala",
                table: "Prova",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prova_IdSalaFK",
                table: "Prova",
                column: "IdSalaFK");

            migrationBuilder.CreateIndex(
                name: "IX_Prova_SalaidSala",
                table: "Prova",
                column: "SalaidSala");

            migrationBuilder.AddForeignKey(
                name: "FK_Prova_Sala_IdSalaFK",
                table: "Prova",
                column: "IdSalaFK",
                principalTable: "Sala",
                principalColumn: "idSala",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prova_Sala_SalaidSala",
                table: "Prova",
                column: "SalaidSala",
                principalTable: "Sala",
                principalColumn: "idSala");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prova_Sala_IdSalaFK",
                table: "Prova");

            migrationBuilder.DropForeignKey(
                name: "FK_Prova_Sala_SalaidSala",
                table: "Prova");

            migrationBuilder.DropIndex(
                name: "IX_Prova_IdSalaFK",
                table: "Prova");

            migrationBuilder.DropIndex(
                name: "IX_Prova_SalaidSala",
                table: "Prova");

            migrationBuilder.DropColumn(
                name: "IdSalaFK",
                table: "Prova");

            migrationBuilder.DropColumn(
                name: "SalaidSala",
                table: "Prova");

            migrationBuilder.CreateIndex(
                name: "IX_Prova_idSala",
                table: "Prova",
                column: "idSala");

            migrationBuilder.AddForeignKey(
                name: "FK_Prova_Sala_idSala",
                table: "Prova",
                column: "idSala",
                principalTable: "Sala",
                principalColumn: "idSala",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
