using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeGotThis.Migrations
{
    /// <inheritdoc />
    public partial class RemoveGoalCompletionFlags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "IsRejected",
                table: "Goals");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "Goals",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRejected",
                table: "Goals",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
