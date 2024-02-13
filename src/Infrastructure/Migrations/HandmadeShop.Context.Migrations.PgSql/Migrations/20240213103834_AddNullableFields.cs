using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HandmadeShop.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class AddNullableFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_Catalogs_CatalogId",
                table: "products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Catalogs",
                table: "Catalogs");

            migrationBuilder.RenameTable(
                name: "Catalogs",
                newName: "catalogs");

            migrationBuilder.RenameIndex(
                name: "IX_Catalogs_Uid",
                table: "catalogs",
                newName: "IX_catalogs_Uid");

            migrationBuilder.AlterColumn<int>(
                name: "Gender",
                table: "users",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "users",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "catalogs",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_catalogs",
                table: "catalogs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_products_catalogs_CatalogId",
                table: "products",
                column: "CatalogId",
                principalTable: "catalogs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_catalogs_CatalogId",
                table: "products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_catalogs",
                table: "catalogs");

            migrationBuilder.RenameTable(
                name: "catalogs",
                newName: "Catalogs");

            migrationBuilder.RenameIndex(
                name: "IX_catalogs_Uid",
                table: "Catalogs",
                newName: "IX_Catalogs_Uid");

            migrationBuilder.AlterColumn<int>(
                name: "Gender",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "users",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Catalogs",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Catalogs",
                table: "Catalogs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_products_Catalogs_CatalogId",
                table: "products",
                column: "CatalogId",
                principalTable: "Catalogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
