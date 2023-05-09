using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookShop.Migrations;

/// <inheritdoc />
public partial class BookSeeds : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(
            table: "Books",
            columns: new[] { "Id", "Author", "Description", "Name", "Pages", "Price", "Year" },
            values: new object[] { 1, "Andrew Lock", "ASP.NET Core in Action opens up the world of cross-platform web development with .NET. You'll start with a crash course in .NET Core, immediately cutting the cord between ASP.NET and Windows. Then, you'll begin to build amazing web applications step by step, systematically adding essential features like logins, configuration, dependency injection, and custom components. Along the way, you'll mix in important process steps like testing, multiplatform deployment, and security.", "ASP.NET Core in Action", 712, 50, 2018 });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "Books",
            keyColumn: "Id",
            keyValue: 1);
    }
}
