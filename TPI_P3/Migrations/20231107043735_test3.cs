using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TPI_P3.Migrations
{
    /// <inheritdoc />
    public partial class test3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SizeId",
                table: "Sizes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ColourId",
                table: "Colours",
                newName: "Id");

            migrationBuilder.UpdateData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: 2,
                column: "SizeName",
                value: "XL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Sizes",
                newName: "SizeId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Colours",
                newName: "ColourId");

            migrationBuilder.UpdateData(
                table: "Sizes",
                keyColumn: "SizeId",
                keyValue: 2,
                column: "SizeName",
                value: "L");
        }
    }
}
