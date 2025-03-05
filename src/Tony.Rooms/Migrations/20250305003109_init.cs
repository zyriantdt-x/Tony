using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tony.Rooms.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "navigator_categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    parent_id = table.Column<int>(type: "INTEGER", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    is_node = table.Column<bool>(type: "INTEGER", nullable: false),
                    is_public_space = table.Column<bool>(type: "INTEGER", nullable: false),
                    is_trading_allowed = table.Column<bool>(type: "INTEGER", nullable: false),
                    min_access = table.Column<int>(type: "INTEGER", nullable: false),
                    min_assign = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_navigator_categories", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "navigator_categories");
        }
    }
}
