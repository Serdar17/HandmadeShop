using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HandmadeShop.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class AddReviewOwner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "reviews");

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "reviews",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_reviews_OwnerId",
                table: "reviews",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_reviews_users_OwnerId",
                table: "reviews",
                column: "OwnerId",
                principalTable: "users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_reviews_users_OwnerId",
                table: "reviews");

            migrationBuilder.DropIndex(
                name: "IX_reviews_OwnerId",
                table: "reviews");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "reviews");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "reviews",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
