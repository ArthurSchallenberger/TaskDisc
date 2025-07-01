using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskDisc.Migrations
{
    /// <inheritdoc />
    public partial class _004 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ID_Token",
                table: "Users");

            migrationBuilder.AddColumn<bool>(
                name: "IsRevoked",
                table: "Tokens",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUsed",
                table: "Tokens",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_Token",
                table: "Tokens",
                column: "Token",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tokens_Token",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "IsRevoked",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "LastUsed",
                table: "Tokens");

            migrationBuilder.AddColumn<int>(
                name: "ID_Token",
                table: "Users",
                type: "integer",
                nullable: true);
        }
    }
}
