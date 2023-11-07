using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TPI_P3.Migrations
{
    /// <inheritdoc />
    public partial class test2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ColoursProducts_Colours_ColoursColourId",
                table: "ColoursProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_SizesProducts_Sizes_SizesSizeId",
                table: "SizesProducts");

            migrationBuilder.RenameColumn(
                name: "SizesSizeId",
                table: "SizesProducts",
                newName: "SizesId");

            migrationBuilder.RenameIndex(
                name: "IX_SizesProducts_SizesSizeId",
                table: "SizesProducts",
                newName: "IX_SizesProducts_SizesId");

            migrationBuilder.RenameColumn(
                name: "ColoursColourId",
                table: "ColoursProducts",
                newName: "ColoursId");

            migrationBuilder.AddForeignKey(
                name: "FK_ColoursProducts_Colours_ColoursId",
                table: "ColoursProducts",
                column: "ColoursId",
                principalTable: "Colours",
                principalColumn: "ColourId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SizesProducts_Sizes_SizesId",
                table: "SizesProducts",
                column: "SizesId",
                principalTable: "Sizes",
                principalColumn: "SizeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ColoursProducts_Colours_ColoursId",
                table: "ColoursProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_SizesProducts_Sizes_SizesId",
                table: "SizesProducts");

            migrationBuilder.RenameColumn(
                name: "SizesId",
                table: "SizesProducts",
                newName: "SizesSizeId");

            migrationBuilder.RenameIndex(
                name: "IX_SizesProducts_SizesId",
                table: "SizesProducts",
                newName: "IX_SizesProducts_SizesSizeId");

            migrationBuilder.RenameColumn(
                name: "ColoursId",
                table: "ColoursProducts",
                newName: "ColoursColourId");

            migrationBuilder.AddForeignKey(
                name: "FK_ColoursProducts_Colours_ColoursColourId",
                table: "ColoursProducts",
                column: "ColoursColourId",
                principalTable: "Colours",
                principalColumn: "ColourId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SizesProducts_Sizes_SizesSizeId",
                table: "SizesProducts",
                column: "SizesSizeId",
                principalTable: "Sizes",
                principalColumn: "SizeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
