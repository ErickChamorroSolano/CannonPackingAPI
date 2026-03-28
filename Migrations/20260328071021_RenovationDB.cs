using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CannonPackingAPI.Migrations
{
    /// <inheritdoc />
    public partial class RenovationDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoxTowel");

            migrationBuilder.RenameColumn(
                name: "TowelStatus",
                table: "Towel",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "BoxStatus",
                table: "Box",
                newName: "Status");

            migrationBuilder.AlterColumn<string>(
                name: "ProductCode",
                table: "Towel",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ProductCode",
                table: "Box",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<short>(
                name: "Capacity",
                table: "Box",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Towel",
                newName: "TowelStatus");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Box",
                newName: "BoxStatus");

            migrationBuilder.AlterColumn<string>(
                name: "ProductCode",
                table: "Towel",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "ProductCode",
                table: "Box",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<int>(
                name: "Capacity",
                table: "Box",
                type: "int",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.CreateTable(
                name: "BoxTowel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BoxId = table.Column<int>(type: "int", nullable: false),
                    TowelId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoxTowel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoxTowel_Box_BoxId",
                        column: x => x.BoxId,
                        principalTable: "Box",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoxTowel_Towel_TowelId",
                        column: x => x.TowelId,
                        principalTable: "Towel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoxTowel_BoxId",
                table: "BoxTowel",
                column: "BoxId");

            migrationBuilder.CreateIndex(
                name: "IX_BoxTowel_TowelId",
                table: "BoxTowel",
                column: "TowelId");
        }
    }
}
