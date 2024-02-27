using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HandmadeShop.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAddressFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address_District",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "Address_HouseNumber",
                table: "orders");

            migrationBuilder.RenameColumn(
                name: "Address_Street",
                table: "orders",
                newName: "Address_ExactAddress");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address_ExactAddress",
                table: "orders",
                newName: "Address_Street");

            migrationBuilder.AddColumn<string>(
                name: "Address_District",
                table: "orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address_HouseNumber",
                table: "orders",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
