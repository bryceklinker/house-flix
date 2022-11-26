using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace House.Flix.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class AddVideoFileEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "VideoFilePath", table: "MovieEntity");

            migrationBuilder.AddColumn<Guid>(
                name: "VideoFileId",
                table: "MovieEntity",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000")
            );

            migrationBuilder.CreateTable(
                name: "VideoFileEntity",
                columns: table =>
                    new
                    {
                        Id = table.Column<Guid>(
                            type: "uuid",
                            nullable: false,
                            defaultValueSql: "gen_random_uuid()"
                        ),
                        Path = table.Column<string>(type: "text", nullable: false),
                        Size = table.Column<long>(type: "bigint", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoFileEntity", x => x.Id);
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_MovieEntity_VideoFileId",
                table: "MovieEntity",
                column: "VideoFileId"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_MovieEntity_VideoFileEntity_VideoFileId",
                table: "MovieEntity",
                column: "VideoFileId",
                principalTable: "VideoFileEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieEntity_VideoFileEntity_VideoFileId",
                table: "MovieEntity"
            );

            migrationBuilder.DropTable(name: "VideoFileEntity");

            migrationBuilder.DropIndex(name: "IX_MovieEntity_VideoFileId", table: "MovieEntity");

            migrationBuilder.DropColumn(name: "VideoFileId", table: "MovieEntity");

            migrationBuilder.AddColumn<string>(
                name: "VideoFilePath",
                table: "MovieEntity",
                type: "text",
                nullable: false,
                defaultValue: ""
            );
        }
    }
}
