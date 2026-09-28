using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeGotThis.Migrations
{
    /// <inheritdoc />
    public partial class AddTokensAndManyGoals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Goals_MemberId",
                table: "Goals");

            migrationBuilder.AddColumn<int>(
                name: "Tokens",
                table: "Members",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Goals_MemberId",
                table: "Goals",
                column: "MemberId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Goals_MemberId",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "Tokens",
                table: "Members");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_MemberId",
                table: "Goals",
                column: "MemberId",
                unique: true);
        }
    }
}
