using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookShop.Migrations;

/// <inheritdoc />
public partial class BookCatId : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<int>(
            name: "CategoryId",
            table: "Books",
            type: "int",
            nullable: false,
            defaultValue: 0,
            oldClrType: typeof(int),
            oldType: "int",
            oldNullable: true);

        migrationBuilder.CreateIndex(
            name: "IX_Books_CategoryId",
            table: "Books",
            column: "CategoryId");

        migrationBuilder.AddForeignKey(
            name: "FK_Books_Categories_CategoryId",
            table: "Books",
            column: "CategoryId",
            principalTable: "Categories",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Books_Categories_CategoryId",
            table: "Books");

        migrationBuilder.DropIndex(
            name: "IX_Books_CategoryId",
            table: "Books");

        migrationBuilder.AlterColumn<int>(
            name: "CategoryId",
            table: "Books",
            type: "int",
            nullable: true,
            oldClrType: typeof(int),
            oldType: "int");
    }
}
