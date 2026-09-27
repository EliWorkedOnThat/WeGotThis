using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeGotThis.Migrations
{
    /// <inheritdoc />
    public partial class AddGoalActions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GoalActions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GoalId = table.Column<int>(type: "INTEGER", nullable: false),
                    MemberId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsCompleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsRejected = table.Column<bool>(type: "INTEGER", nullable: false),
                    ActedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoalActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoalActions_Goals_GoalId",
                        column: x => x.GoalId,
                        principalTable: "Goals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GoalActions_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GoalActions_GoalId",
                table: "GoalActions",
                column: "GoalId");

            migrationBuilder.CreateIndex(
                name: "IX_GoalActions_MemberId",
                table: "GoalActions",
                column: "MemberId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GoalActions");
        }
    }
}
