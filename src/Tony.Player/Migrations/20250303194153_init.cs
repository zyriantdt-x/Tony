using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tony.Player.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "player_data",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    username = table.Column<string>(type: "TEXT", nullable: false),
                    password = table.Column<string>(type: "TEXT", nullable: false),
                    credits = table.Column<int>(type: "INTEGER", nullable: false),
                    figure = table.Column<string>(type: "TEXT", nullable: false),
                    sex = table.Column<bool>(type: "INTEGER", nullable: false),
                    mission = table.Column<string>(type: "TEXT", nullable: false),
                    tickets = table.Column<int>(type: "INTEGER", nullable: false),
                    pool_figure = table.Column<string>(type: "TEXT", nullable: false),
                    film = table.Column<int>(type: "INTEGER", nullable: false),
                    receive_news = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_player_data", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "player_data");
        }
    }
}
