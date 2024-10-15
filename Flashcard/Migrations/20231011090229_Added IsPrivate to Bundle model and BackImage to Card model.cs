using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flashcard.Migrations
{
    public partial class AddedIsPrivatetoBundlemodelandBackImagetoCardmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BackImage",
                schema: "dbo",
                table: "Cards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPrivate",
                schema: "dbo",
                table: "Bundles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackImage",
                schema: "dbo",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "IsPrivate",
                schema: "dbo",
                table: "Bundles");
        }
    }
}
