using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_photo_gallery.Migrations
{
    /// <inheritdoc />
    public partial class AtualizacaoAppDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gallery_Users_UserId",
                table: "Gallery");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Gallery",
                table: "Gallery");

            migrationBuilder.RenameTable(
                name: "Gallery",
                newName: "Galleries");

            migrationBuilder.RenameIndex(
                name: "IX_Gallery_UserId",
                table: "Galleries",
                newName: "IX_Galleries_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Galleries",
                table: "Galleries",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Galleries_Users_UserId",
                table: "Galleries",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Galleries_Users_UserId",
                table: "Galleries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Galleries",
                table: "Galleries");

            migrationBuilder.RenameTable(
                name: "Galleries",
                newName: "Gallery");

            migrationBuilder.RenameIndex(
                name: "IX_Galleries_UserId",
                table: "Gallery",
                newName: "IX_Gallery_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Gallery",
                table: "Gallery",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Gallery_Users_UserId",
                table: "Gallery",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
