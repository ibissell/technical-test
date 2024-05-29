using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bissell.Database.Migrations
{
    /// <inheritdoc />
    public partial class create : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsertedDttm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDttm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Forename = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelephoneNo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.PersonId);
                });

            migrationBuilder.CreateTable(
                name: "Bugs",
                columns: table => new
                {
                    BugId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssignedPersonId = table.Column<int>(type: "int", nullable: true),
                    InsertedDttm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDttm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bugs", x => x.BugId);
                    table.ForeignKey(
                        name: "FK_Bugs_Persons_AssignedPersonId",
                        column: x => x.AssignedPersonId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BugsHistory",
                columns: table => new
                {
                    BugHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BugId = table.Column<int>(type: "int", nullable: false),
                    AssignedPersonId = table.Column<int>(type: "int", nullable: true),
                    InsertedDttm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDttm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BugsHistory", x => x.BugHistoryId);
                    table.ForeignKey(
                        name: "FK_BugsHistory_Bugs_BugId",
                        column: x => x.BugId,
                        principalTable: "Bugs",
                        principalColumn: "BugId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BugsHistory_Persons_AssignedPersonId",
                        column: x => x.AssignedPersonId,
                        principalTable: "Persons",
                        principalColumn: "PersonId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bugs_AssignedPersonId",
                table: "Bugs",
                column: "AssignedPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_BugsHistory_AssignedPersonId",
                table: "BugsHistory",
                column: "AssignedPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_BugsHistory_BugId",
                table: "BugsHistory",
                column: "BugId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BugsHistory");

            migrationBuilder.DropTable(
                name: "Bugs");

            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
