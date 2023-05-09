using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookShop.Migrations;

/// <inheritdoc />
public partial class BookAuthorDetails : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "AuthorDetails",
            table: "Books",
            type: "nvarchar(max)",
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "AuthorDetails",
            table: "Books");
    }
}
