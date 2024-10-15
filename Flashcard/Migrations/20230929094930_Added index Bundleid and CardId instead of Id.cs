using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flashcard.Migrations
{
    public partial class AddedindexBundleidandCardIdinsteadofId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Cards_BundleId_CardId",
                schema: "dbo",
                table: "Cards",
                columns: new[] { "BundleId", "CardId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cards_BundleId_CardId",
                schema: "dbo",
                table: "Cards");
        }
    }
}
