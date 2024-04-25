using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmallBusinessSystem.Migrations
{
    /// <inheritdoc />
    public partial class MaybenowPlease : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_AspNetUsers_ApplicationUserId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Candies_CandyId",
                table: "Carts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Carts",
                table: "Carts");

            migrationBuilder.RenameTable(
                name: "Carts",
                newName: "CartNew");

            migrationBuilder.RenameIndex(
                name: "IX_Carts_CandyId",
                table: "CartNew",
                newName: "IX_CartNew_CandyId");

            migrationBuilder.RenameIndex(
                name: "IX_Carts_ApplicationUserId",
                table: "CartNew",
                newName: "IX_CartNew_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartNew",
                table: "CartNew",
                column: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartNew_AspNetUsers_ApplicationUserId",
                table: "CartNew",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartNew_Candies_CandyId",
                table: "CartNew",
                column: "CandyId",
                principalTable: "Candies",
                principalColumn: "CandyId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartNew_AspNetUsers_ApplicationUserId",
                table: "CartNew");

            migrationBuilder.DropForeignKey(
                name: "FK_CartNew_Candies_CandyId",
                table: "CartNew");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartNew",
                table: "CartNew");

            migrationBuilder.RenameTable(
                name: "CartNew",
                newName: "Carts");

            migrationBuilder.RenameIndex(
                name: "IX_CartNew_CandyId",
                table: "Carts",
                newName: "IX_Carts_CandyId");

            migrationBuilder.RenameIndex(
                name: "IX_CartNew_ApplicationUserId",
                table: "Carts",
                newName: "IX_Carts_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Carts",
                table: "Carts",
                column: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_AspNetUsers_ApplicationUserId",
                table: "Carts",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Candies_CandyId",
                table: "Carts",
                column: "CandyId",
                principalTable: "Candies",
                principalColumn: "CandyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
