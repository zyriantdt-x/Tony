using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tony.Server.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
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

            migrationBuilder.CreateTable(
                name: "room_models",
                columns: table => new
                {
                    model_id = table.Column<string>(type: "TEXT", nullable: false),
                    door_x = table.Column<int>(type: "INTEGER", nullable: false),
                    door_y = table.Column<int>(type: "INTEGER", nullable: false),
                    door_z = table.Column<int>(type: "INTEGER", nullable: false),
                    door_dir = table.Column<int>(type: "INTEGER", nullable: false),
                    heightmap = table.Column<string>(type: "TEXT", nullable: false),
                    trigger_class = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_room_models", x => x.model_id);
                });

            migrationBuilder.CreateTable(
                name: "room_data",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    owner_id = table.Column<int>(type: "INTEGER", nullable: false),
                    category = table.Column<int>(type: "INTEGER", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", nullable: false),
                    description = table.Column<string>(type: "varchar(255)", nullable: false),
                    model = table.Column<string>(type: "varchar(255)", nullable: false),
                    ccts = table.Column<string>(type: "varchar(255)", nullable: false),
                    wallpaper = table.Column<int>(type: "INTEGER", nullable: false),
                    floor = table.Column<int>(type: "INTEGER", nullable: false),
                    showname = table.Column<bool>(type: "INTEGER", nullable: false),
                    superusers = table.Column<bool>(type: "INTEGER", nullable: false),
                    accesstype = table.Column<int>(type: "INTEGER", nullable: false),
                    password = table.Column<string>(type: "varchar(255)", nullable: false),
                    visitors_now = table.Column<int>(type: "INTEGER", nullable: false),
                    visitors_max = table.Column<int>(type: "INTEGER", nullable: false),
                    rating = table.Column<int>(type: "INTEGER", nullable: false),
                    is_hidden = table.Column<bool>(type: "INTEGER", nullable: false),
                    created_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updated_at = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_room_data", x => x.id);
                    table.ForeignKey(
                        name: "FK_room_data_player_data_owner_id",
                        column: x => x.owner_id,
                        principalTable: "player_data",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_room_data_room_models_model",
                        column: x => x.model,
                        principalTable: "room_models",
                        principalColumn: "model_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_room_data_model",
                table: "room_data",
                column: "model");

            migrationBuilder.CreateIndex(
                name: "IX_room_data_owner_id",
                table: "room_data",
                column: "owner_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "navigator_categories");

            migrationBuilder.DropTable(
                name: "room_data");

            migrationBuilder.DropTable(
                name: "player_data");

            migrationBuilder.DropTable(
                name: "room_models");
        }
    }
}
