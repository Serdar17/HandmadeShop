using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HandmadeShop.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserLikeEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_likes_likes_LikesId",
                table: "users_likes");

            migrationBuilder.DropForeignKey(
                name: "FK_users_likes_users_UsersId",
                table: "users_likes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users_likes",
                table: "users_likes");

            migrationBuilder.DropIndex(
                name: "IX_users_likes_UsersId",
                table: "users_likes");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "users_likes",
                newName: "Uid");

            migrationBuilder.RenameColumn(
                name: "LikesId",
                table: "users_likes",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "users_likes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "LikeId",
                table: "users_likes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "users_likes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "users_likes",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "users_likes",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_users_likes",
                table: "users_likes",
                columns: new[] { "LikeId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_users_likes_Uid",
                table: "users_likes",
                column: "Uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_likes_UserId",
                table: "users_likes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_users_likes_likes_LikeId",
                table: "users_likes",
                column: "LikeId",
                principalTable: "likes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_users_likes_users_UserId",
                table: "users_likes",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_likes_likes_LikeId",
                table: "users_likes");

            migrationBuilder.DropForeignKey(
                name: "FK_users_likes_users_UserId",
                table: "users_likes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users_likes",
                table: "users_likes");

            migrationBuilder.DropIndex(
                name: "IX_users_likes_Uid",
                table: "users_likes");

            migrationBuilder.DropIndex(
                name: "IX_users_likes_UserId",
                table: "users_likes");

            migrationBuilder.DropColumn(
                name: "LikeId",
                table: "users_likes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "users_likes");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "users_likes");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "users_likes");

            migrationBuilder.RenameColumn(
                name: "Uid",
                table: "users_likes",
                newName: "UsersId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "users_likes",
                newName: "LikesId");

            migrationBuilder.AlterColumn<int>(
                name: "LikesId",
                table: "users_likes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_users_likes",
                table: "users_likes",
                columns: new[] { "LikesId", "UsersId" });

            migrationBuilder.CreateIndex(
                name: "IX_users_likes_UsersId",
                table: "users_likes",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_users_likes_likes_LikesId",
                table: "users_likes",
                column: "LikesId",
                principalTable: "likes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_users_likes_users_UsersId",
                table: "users_likes",
                column: "UsersId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
