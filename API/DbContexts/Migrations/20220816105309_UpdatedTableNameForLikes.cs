using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.DbContexts.Migrations
{
    public partial class UpdatedTableNameForLikes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLike_Users_LikedUserId",
                table: "UserLike");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLike_Users_SourceUserId",
                table: "UserLike");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserLike",
                table: "UserLike");

            migrationBuilder.RenameTable(
                name: "UserLike",
                newName: "Likes");

            migrationBuilder.RenameIndex(
                name: "IX_UserLike_LikedUserId",
                table: "Likes",
                newName: "IX_Likes_LikedUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Likes",
                table: "Likes",
                columns: new[] { "SourceUserId", "LikedUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Users_LikedUserId",
                table: "Likes",
                column: "LikedUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Users_SourceUserId",
                table: "Likes",
                column: "SourceUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Users_LikedUserId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Users_SourceUserId",
                table: "Likes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Likes",
                table: "Likes");

            migrationBuilder.RenameTable(
                name: "Likes",
                newName: "UserLike");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_LikedUserId",
                table: "UserLike",
                newName: "IX_UserLike_LikedUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserLike",
                table: "UserLike",
                columns: new[] { "SourceUserId", "LikedUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserLike_Users_LikedUserId",
                table: "UserLike",
                column: "LikedUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLike_Users_SourceUserId",
                table: "UserLike",
                column: "SourceUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
