using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CannonPackingAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Box",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BoxCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    BoxStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Box", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Towel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TowelStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BoxId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Towel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Towel_Box_BoxId",
                        column: x => x.BoxId,
                        principalTable: "Box",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "IX_Box_BoxCode",
                table: "Box",
                column: "BoxCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BoxTowel_BoxId",
                table: "BoxTowel",
                column: "BoxId");

            migrationBuilder.CreateIndex(
                name: "IX_BoxTowel_TowelId",
                table: "BoxTowel",
                column: "TowelId");

            migrationBuilder.CreateIndex(
                name: "IX_Towel_BoxId",
                table: "Towel",
                column: "BoxId");

            migrationBuilder.CreateIndex(
                name: "IX_Towel_ItemCode",
                table: "Towel",
                column: "ItemCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoxTowel");

            migrationBuilder.DropTable(
                name: "Towel");

            migrationBuilder.DropTable(
                name: "Box");
        }
    }
}
