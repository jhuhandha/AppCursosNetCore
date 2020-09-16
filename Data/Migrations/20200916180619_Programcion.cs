using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppWebSena.Data.Migrations
{
    public partial class Programcion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Imagen",
                table: "Curso",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "ProgramacionCurso",
                columns: table => new
                {
                    ProgramacionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(nullable: false),
                    HoraInicio = table.Column<string>(nullable: true),
                    HoraFinal = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    CursoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramacionCurso", x => x.ProgramacionId);
                    table.ForeignKey(
                        name: "FK_ProgramacionCurso_Curso_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Curso",
                        principalColumn: "CursoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProgramacionCurso_CursoId",
                table: "ProgramacionCurso",
                column: "CursoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProgramacionCurso");

            migrationBuilder.AlterColumn<string>(
                name: "Imagen",
                table: "Curso",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
