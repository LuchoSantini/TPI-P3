using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TPI_P3.Migrations
{
    /// <inheritdoc />
    public partial class VariantsAcceptedInOrderLine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ColourId",
                table: "OrderLines",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SizeId",
                table: "OrderLines",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderLines_ColourId",
                table: "OrderLines",
                column: "ColourId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLines_SizeId",
                table: "OrderLines",
                column: "SizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLines_Colours_ColourId",
                table: "OrderLines",
                column: "ColourId",
                principalTable: "Colours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLines_Sizes_SizeId",
                table: "OrderLines",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderLines_Colours_ColourId",
                table: "OrderLines");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderLines_Sizes_SizeId",
                table: "OrderLines");

            migrationBuilder.DropIndex(
                name: "IX_OrderLines_ColourId",
                table: "OrderLines");

            migrationBuilder.DropIndex(
                name: "IX_OrderLines_SizeId",
                table: "OrderLines");

            migrationBuilder.DropColumn(
                name: "ColourId",
                table: "OrderLines");

            migrationBuilder.DropColumn(
                name: "SizeId",
                table: "OrderLines");
        }
    }
}
