using BookShop.Infrastructure.DataModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace BookShop.Infrastructure;
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<BookCategory> Categories { get; set; }
    public DbSet<BookData> Books { get; set; }
    public DbSet<OrderData> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<BookCategory>().HasData(
            new BookCategory { Id = 1, Name = "Technical" },
            new BookCategory { Id = 2, Name = "Fiction" },
            new BookCategory { Id = 3, Name = "Children" },
            new BookCategory { Id = 4, Name = "Novel" });

       
        base.OnModelCreating(builder);
    }
}