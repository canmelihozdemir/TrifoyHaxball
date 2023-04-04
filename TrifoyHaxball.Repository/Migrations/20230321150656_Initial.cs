using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrifoyHaxball.Repository.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Role = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    PlayedGameCount = table.Column<int>(type: "int", nullable: true),
                    HighScore = table.Column<int>(type: "int", nullable: true),
                    Coin = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlayerRanks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Point = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerRanks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerRanks_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "Coin", "CreatedDate", "HighScore", "Name", "Password", "PlayedGameCount", "Role", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, 0, new DateTime(2023, 3, 21, 18, 6, 56, 173, DateTimeKind.Local).AddTicks(4467), 0, "Trifoy", "testTr12123", 0, "player", null },
                    { 2, 3, new DateTime(2023, 3, 21, 18, 6, 56, 173, DateTimeKind.Local).AddTicks(4486), 2, "Admin", "testTr12123", 1, "player", null }
                });

            migrationBuilder.InsertData(
                table: "PlayerRanks",
                columns: new[] { "Id", "Name", "PlayerId", "Point" },
                values: new object[] { 1, "Yeni", 1, 0 });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerRanks_PlayerId",
                table: "PlayerRanks",
                column: "PlayerId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerRanks");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
