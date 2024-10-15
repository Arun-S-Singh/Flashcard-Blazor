using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flashcard.Migrations
{
    public partial class Addedsubscriptionidanddurationfrombundle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Subscriptions",
                schema: "dbo",
                table: "Subscriptions");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                schema: "dbo",
                table: "Subscriptions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionPeriod",
                schema: "dbo",
                table: "Bundles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subscriptions",
                schema: "dbo",
                table: "Subscriptions",
                columns: new[] { "Id", "UserId", "BundleID" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Subscriptions",
                schema: "dbo",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "dbo",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "SubscriptionPeriod",
                schema: "dbo",
                table: "Bundles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subscriptions",
                schema: "dbo",
                table: "Subscriptions",
                columns: new[] { "UserId", "BundleID" });
        }
    }
}
