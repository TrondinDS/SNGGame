using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudioGameService.Migrations
{
    /// <inheritdoc />
    public partial class CreatDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Studios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    IsResolutionPublication = table.Column<bool>(type: "boolean", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DateDeleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Studios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RussianTitle = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    EnglishTitle = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    AlternativeTitle = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ShortDescription = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    LinkSite = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Publisher = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CountryDevelopment = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    LinkPageStore = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Platform = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DateDeleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    StudioId = table.Column<Guid>(type: "uuid", nullable: false),
                    StatisticGameId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_Studios_StudioId",
                        column: x => x.StudioId,
                        principalTable: "Studios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameLibraries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Rating = table.Column<int>(type: "integer", nullable: false),
                    IsBought = table.Column<bool>(type: "boolean", nullable: false),
                    GameId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameLibraries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameLibraries_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameSelectedGenres",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NumberOrder = table.Column<int>(type: "integer", nullable: false),
                    GameId = table.Column<Guid>(type: "uuid", nullable: false),
                    GenreId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameSelectedGenres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameSelectedGenres_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameSelectedGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameSelectedTags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GameId = table.Column<Guid>(type: "uuid", nullable: false),
                    TagId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameSelectedTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameSelectedTags_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameSelectedTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "statisticGames",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RatingSum = table.Column<int>(type: "integer", nullable: false),
                    PeopleCount = table.Column<int>(type: "integer", nullable: false),
                    GameId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_statisticGames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_statisticGames_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameLibraries_GameId",
                table: "GameLibraries",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_StudioId",
                table: "Games",
                column: "StudioId");

            migrationBuilder.CreateIndex(
                name: "IX_GameSelectedGenres_GameId",
                table: "GameSelectedGenres",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameSelectedGenres_GenreId",
                table: "GameSelectedGenres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_GameSelectedTags_GameId",
                table: "GameSelectedTags",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameSelectedTags_TagId",
                table: "GameSelectedTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_statisticGames_GameId",
                table: "statisticGames",
                column: "GameId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameLibraries");

            migrationBuilder.DropTable(
                name: "GameSelectedGenres");

            migrationBuilder.DropTable(
                name: "GameSelectedTags");

            migrationBuilder.DropTable(
                name: "statisticGames");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Studios");
        }
    }
}
