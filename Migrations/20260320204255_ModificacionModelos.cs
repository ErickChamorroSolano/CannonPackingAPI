using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CannonPackingAPI.Migrations
{
    /// <inheritdoc />
    public partial class ModificacionModelos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoxTowel_Box_BoxId",
                table: "BoxTowel");

            migrationBuilder.DropForeignKey(
                name: "FK_BoxTowel_Towel_TowelId",
                table: "BoxTowel");

            migrationBuilder.DropForeignKey(
                name: "FK_Towel_Box_BoxId",
                table: "Towel");

            migrationBuilder.DropIndex(
                name: "IX_Towel_BoxId",
                table: "Towel");

            migrationBuilder.DropIndex(
                name: "IX_BoxTowel_BoxId",
                table: "BoxTowel");

            migrationBuilder.DropColumn(
                name: "BoxId",
                table: "Towel");

            migrationBuilder.CreateIndex(
                name: "IX_BoxTowel_BoxId_TowelId",
                table: "BoxTowel",
                columns: new[] { "BoxId", "TowelId" },
                unique: true,
                filter: "IsActive = 1");

            migrationBuilder.AddForeignKey(
                name: "FK_BoxTowel_Box_BoxId",
                table: "BoxTowel",
                column: "BoxId",
                principalTable: "Box",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BoxTowel_Towel_TowelId",
                table: "BoxTowel",
                column: "TowelId",
                principalTable: "Towel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoxTowel_Box_BoxId",
                table: "BoxTowel");

            migrationBuilder.DropForeignKey(
                name: "FK_BoxTowel_Towel_TowelId",
                table: "BoxTowel");

            migrationBuilder.DropIndex(
                name: "IX_BoxTowel_BoxId_TowelId",
                table: "BoxTowel");

            migrationBuilder.AddColumn<int>(
                name: "BoxId",
                table: "Towel",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Towel_BoxId",
                table: "Towel",
                column: "BoxId");

            migrationBuilder.CreateIndex(
                name: "IX_BoxTowel_BoxId",
                table: "BoxTowel",
                column: "BoxId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoxTowel_Box_BoxId",
                table: "BoxTowel",
                column: "BoxId",
                principalTable: "Box",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BoxTowel_Towel_TowelId",
                table: "BoxTowel",
                column: "TowelId",
                principalTable: "Towel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Towel_Box_BoxId",
                table: "Towel",
                column: "BoxId",
                principalTable: "Box",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
