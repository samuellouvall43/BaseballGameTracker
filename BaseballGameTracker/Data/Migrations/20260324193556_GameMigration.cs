using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseballGameTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class GameMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardinalsScore = table.Column<int>(type: "int", nullable: false),
                    OpposingTeamScore = table.Column<int>(type: "int", nullable: false),
                    OpponentName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Game");
        }
    }
}
