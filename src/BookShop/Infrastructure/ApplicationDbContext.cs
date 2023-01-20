using BookShop.Infrastructure.DataModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace BookShop.Infrastructure;
public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<BookCategory> Categories { get; set; }
    public DbSet<BookData> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<BookCategory>().HasData(
            new BookCategory { Id = 1, Name = "Technical" },
            new BookCategory { Id = 2, Name = "Fiction" },
            new BookCategory { Id = 3, Name = "Children" },
            new BookCategory { Id = 4, Name = "Novel" });

        //builder.Entity<BookData>().HasData(new BookData
        //{
        //    Id = 1,
        //    Author = "Andrew Lock",
        //    Description = "ASP.NET Core in Action opens up the world of cross-platform web development with .NET. You'll start with a crash course in .NET Core, immediately cutting the cord between ASP.NET and Windows. Then, you'll begin to build amazing web applications step by step, systematically adding essential features like logins, configuration, dependency injection, and custom components. Along the way, you'll mix in important process steps like testing, multiplatform deployment, and security.",
        //    Name = "ASP.NET Core in Action",
        //    Pages = 712,
        //    Year = 2018,
        //    Price = 50,
        //    //BookCategoryId = 1,
        //    Language = LanguageType.English,
        //});

        base.OnModelCreating(builder);
    }
}