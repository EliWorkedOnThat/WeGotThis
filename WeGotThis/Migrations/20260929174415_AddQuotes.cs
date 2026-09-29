using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WeGotThis.Migrations
{
    /// <inheritdoc />
    public partial class AddQuotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Quotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Text = table.Column<string>(type: "TEXT", nullable: false),
                    Author = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quotes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Quotes",
                columns: new[] { "Id", "Author", "Text" },
                values: new object[,]
                {
                    { 1, "Marcus Aurelius", "You have power over your mind, not outside events. Realize this, and you will find strength." },
                    { 2, "Marcus Aurelius", "The impediment to action advances action. What stands in the way becomes the way." },
                    { 3, "Seneca", "We suffer more often in imagination than in reality." },
                    { 4, "Seneca", "Luck is what happens when preparation meets opportunity." },
                    { 5, "Epictetus", "It's not what happens to you, but how you react to it that matters." },
                    { 6, "Epictetus", "No person is free who is not master of themselves." },
                    { 7, "Marcus Aurelius", "Waste no more time arguing what a good person should be. Be one." },
                    { 8, "Seneca", "He who fears death will never do anything worthy of a living person." },
                    { 9, "Epictetus", "First say to yourself what you would be, then do what you have to do." },
                    { 10, "Marcus Aurelius", "The best revenge is not to be like your enemy." }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Quotes");
        }
    }
}
