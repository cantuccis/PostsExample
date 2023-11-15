using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TagEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostEntityTagEntity",
                columns: table => new
                {
                    PostsId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostEntityTagEntity", x => new { x.PostsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_PostEntityTagEntity_PostEntities_PostsId",
                        column: x => x.PostsId,
                        principalTable: "PostEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostEntityTagEntity_TagEntities_TagsId",
                        column: x => x.TagsId,
                        principalTable: "TagEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostEntityTagEntity_TagsId",
                table: "PostEntityTagEntity",
                column: "TagsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostEntityTagEntity");

            migrationBuilder.DropTable(
                name: "PostEntities");

            migrationBuilder.DropTable(
                name: "TagEntities");
        }
    }
}
