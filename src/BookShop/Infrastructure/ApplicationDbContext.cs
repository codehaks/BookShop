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

    public DbSet<CommentData> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<BookCategory>().HasData(
            new BookCategory { Id = 1, Name = "Technical" },
            new BookCategory { Id = 2, Name = "Fiction" },
            new BookCategory { Id = 3, Name = "Children" },
            new BookCategory { Id = 4, Name = "Novel" });


        builder.Entity<BookData>()
            .HasMany(b => b.Ratings)
            .WithOne(r => r.Book).OnDelete(DeleteBehavior.NoAction);

        builder.Entity<OrderData>()
            .HasOne(b => b.Rating)
            .WithOne(r => r.Order).OnDelete(DeleteBehavior.Cascade);

        builder.Entity<RatingData>().HasKey(r => new
        {
            r.OrderId,
            r.BookId
        });

        builder.Entity<BookData>()
            .OwnsOne(b => b.AuthorDetails,
                builder =>
                {
                    builder.ToJson();
                });

        base.OnModelCreating(builder);
    }
}