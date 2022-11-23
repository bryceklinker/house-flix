using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace House.Flix.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class InitialDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovieEntity",
                columns: table =>
                    new
                    {
                        Id = table.Column<Guid>(
                            type: "uuid",
                            nullable: false,
                            defaultValueSql: "gen_random_uuid()"
                        ),
                        Title = table.Column<string>(type: "text", nullable: false),
                        Plot = table.Column<string>(type: "text", nullable: false),
                        Rating = table.Column<string>(type: "text", nullable: false),
                        VideoFilePath = table.Column<string>(type: "text", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieEntity", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "PersonEntity",
                columns: table =>
                    new
                    {
                        Id = table.Column<Guid>(
                            type: "uuid",
                            nullable: false,
                            defaultValueSql: "gen_random_uuid()"
                        ),
                        FirstName = table.Column<string>(type: "text", nullable: false),
                        LastName = table.Column<string>(type: "text", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonEntity", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "RoleEntity",
                columns: table =>
                    new
                    {
                        Id = table.Column<Guid>(
                            type: "uuid",
                            nullable: false,
                            defaultValueSql: "gen_random_uuid()"
                        ),
                        Name = table.Column<string>(type: "text", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleEntity", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "MovieRoleEntity",
                columns: table =>
                    new
                    {
                        Id = table.Column<Guid>(
                            type: "uuid",
                            nullable: false,
                            defaultValueSql: "gen_random_uuid()"
                        ),
                        PersonId = table.Column<Guid>(type: "uuid", nullable: false),
                        RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                        MovieId = table.Column<Guid>(type: "uuid", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieRoleEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieRoleEntity_MovieEntity_MovieId",
                        column: x => x.MovieId,
                        principalTable: "MovieEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_MovieRoleEntity_PersonEntity_PersonId",
                        column: x => x.PersonId,
                        principalTable: "PersonEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_MovieRoleEntity_RoleEntity_RoleId",
                        column: x => x.RoleId,
                        principalTable: "RoleEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_MovieRoleEntity_MovieId",
                table: "MovieRoleEntity",
                column: "MovieId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_MovieRoleEntity_PersonId",
                table: "MovieRoleEntity",
                column: "PersonId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_MovieRoleEntity_RoleId",
                table: "MovieRoleEntity",
                column: "RoleId"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "MovieRoleEntity");

            migrationBuilder.DropTable(name: "MovieEntity");

            migrationBuilder.DropTable(name: "PersonEntity");

            migrationBuilder.DropTable(name: "RoleEntity");
        }
    }
}
