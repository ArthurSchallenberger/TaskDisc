using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Api_Restful.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cargos",
                columns: table => new
                {
                    ID_PK = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Abreviacao = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargos", x => x.ID_PK);
                });

            migrationBuilder.CreateTable(
                name: "Tarefas",
                columns: table => new
                {
                    ID_PK = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Data_Criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Data_Conclusao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    Prioridade = table.Column<int>(type: "integer", nullable: false),
                    Assunto = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarefas", x => x.ID_PK);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    ID_PK = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Senha = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    ID_Cargo = table.Column<int>(type: "integer", nullable: false),
                    ID_Tokens = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.ID_PK);
                    table.ForeignKey(
                        name: "FK_Usuarios_Cargos_ID_Cargo",
                        column: x => x.ID_Cargo,
                        principalTable: "Cargos",
                        principalColumn: "ID_PK",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tokens",
                columns: table => new
                {
                    ID_PK = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Data_Criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ID_Usuario = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.ID_PK);
                    table.ForeignKey(
                        name: "FK_Tokens_Usuarios_ID_Usuario",
                        column: x => x.ID_Usuario,
                        principalTable: "Usuarios",
                        principalColumn: "ID_PK",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioTarefas",
                columns: table => new
                {
                    ID_Usuario = table.Column<int>(type: "integer", nullable: false),
                    ID_Tarefa = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioTarefas", x => new { x.ID_Usuario, x.ID_Tarefa });
                    table.ForeignKey(
                        name: "FK_UsuarioTarefas_Tarefas_ID_Tarefa",
                        column: x => x.ID_Tarefa,
                        principalTable: "Tarefas",
                        principalColumn: "ID_PK",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioTarefas_Usuarios_ID_Usuario",
                        column: x => x.ID_Usuario,
                        principalTable: "Usuarios",
                        principalColumn: "ID_PK",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_ID_Usuario",
                table: "Tokens",
                column: "ID_Usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_ID_Cargo",
                table: "Usuarios",
                column: "ID_Cargo");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioTarefas_ID_Tarefa",
                table: "UsuarioTarefas",
                column: "ID_Tarefa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tokens");

            migrationBuilder.DropTable(
                name: "UsuarioTarefas");

            migrationBuilder.DropTable(
                name: "Tarefas");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Cargos");
        }
    }
}
