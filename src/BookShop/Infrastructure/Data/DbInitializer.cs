using BookShop.Infrastructure.DataModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookShop.Infrastructure.Data;

/// <summary>
/// Initializes the database with seed data including roles, users, books, orders, comments, and ratings.
/// </summary>
public class DbInitializer
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ILogger<DbInitializer> _logger;

    public DbInitializer(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        ILogger<DbInitializer> logger)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
    }

    /// <summary>
    /// Seeds the database with initial data if it hasn't been seeded before.
    /// This method is idempotent and safe to call multiple times.
    /// </summary>
    public async Task SeedAsync()
    {
        try
        {
            // Ensure database is created
            await _context.Database.MigrateAsync();

            // Check if already seeded
            if (await _context.Users.AnyAsync())
            {
                _logger.LogInformation("Database already seeded. Skipping seed operation.");
                return;
            }

            _logger.LogInformation("Starting database seeding...");

            // Seed in order of dependencies
            await SeedRolesAsync();
            await SeedUsersAsync();
            await SeedBooksAsync();
            await SeedOrdersAsync();
            await SeedCommentsAndRatingsAsync();

            _logger.LogInformation("Database seeding completed successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    private async Task SeedRolesAsync()
    {
        _logger.LogInformation("Seeding roles...");

        string[] roles = { "Admin", "User" };

        foreach (var roleName in roles)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
                if (result.Succeeded)
                {
                    _logger.LogInformation("Created role: {RoleName}", roleName);
                }
                else
                {
                    _logger.LogError("Failed to create role: {RoleName}. Errors: {Errors}",
                        roleName, string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
        }
    }

    private async Task SeedUsersAsync()
    {
        _logger.LogInformation("Seeding users...");

        // Create Admin User
        var admin = new ApplicationUser
        {
            UserName = "admin@bookshop.com",
            Email = "admin@bookshop.com",
            EmailConfirmed = true,
            FirstName = "Admin",
            LastName = "User"
        };

        var adminResult = await _userManager.CreateAsync(admin, "Admin@123");
        if (adminResult.Succeeded)
        {
            await _userManager.AddToRoleAsync(admin, "Admin");
            _logger.LogInformation("Created admin user: {Email}", admin.Email);
        }

        // Create Regular Users
        var users = new[]
        {
            new { Email = "user1@bookshop.com", FirstName = "John", LastName = "Doe" },
            new { Email = "user2@bookshop.com", FirstName = "Jane", LastName = "Smith" },
            new { Email = "user3@bookshop.com", FirstName = "Bob", LastName = "Johnson" },
            new { Email = "user4@bookshop.com", FirstName = "Alice", LastName = "Williams" },
            new { Email = "user5@bookshop.com", FirstName = "Charlie", LastName = "Brown" }
        };

        foreach (var userData in users)
        {
            var user = new ApplicationUser
            {
                UserName = userData.Email,
                Email = userData.Email,
                EmailConfirmed = true,
                FirstName = userData.FirstName,
                LastName = userData.LastName
            };

            var result = await _userManager.CreateAsync(user, "User@123");
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                _logger.LogInformation("Created user: {Email}", user.Email);
            }
        }
    }

    private async Task SeedBooksAsync()
    {
        _logger.LogInformation("Seeding books...");

        var books = new List<BookData>
        {
            // Technical Books
            new BookData
            {
                Name = "Clean Code: A Handbook of Agile Software Craftsmanship",
                Author = "Robert C. Martin",
                FileName = "clean-code.pdf",
                Price = 43,
                Description = "Even bad code can function. But if code isn't clean, it can bring a development organization to its knees.",
                Year = 2008,
                Pages = 464,
                CategoryId = 1,
                Language = LanguageType.English
            },
            new BookData
            {
                Name = "Design Patterns: Elements of Reusable Object-Oriented Software",
                Author = "Erich Gamma, Richard Helm, Ralph Johnson, John Vlissides",
                FileName = "design-patterns.pdf",
                Price = 55,
                Description = "Capturing a wealth of experience about the design of object-oriented software, four top-notch designers present a catalog of simple and succinct solutions to commonly occurring design problems.",
                Year = 1994,
                Pages = 395,
                CategoryId = 1,
                Language = LanguageType.English
            },
            new BookData
            {
                Name = "The Pragmatic Programmer: Your Journey to Mastery",
                Author = "David Thomas, Andrew Hunt",
                FileName = "pragmatic-programmer.pdf",
                Price = 45,
                Description = "The Pragmatic Programmer is one of those rare tech books you'll read, re-read, and read again over the years.",
                Year = 2019,
                Pages = 352,
                CategoryId = 1,
                Language = LanguageType.English
            },
            new BookData
            {
                Name = "Introduction to Algorithms",
                Author = "Thomas H. Cormen, Charles E. Leiserson",
                FileName = "intro-algorithms.pdf",
                Price = 90,
                Description = "Some books on algorithms are rigorous but incomplete; others cover masses of material but lack rigor. Introduction to Algorithms uniquely combines rigor and comprehensiveness.",
                Year = 2009,
                Pages = 1312,
                CategoryId = 1,
                Language = LanguageType.English
            },
            new BookData
            {
                Name = "You Don't Know JS: Scope & Closures",
                Author = "Kyle Simpson",
                FileName = "ydkjs-scope.pdf",
                Price = 30,
                Description = "No matter how much experience you have with JavaScript, odds are you don't fully understand the language.",
                Year = 2014,
                Pages = 98,
                CategoryId = 1,
                Language = LanguageType.English
            },

            // Fiction Books
            new BookData
            {
                Name = "The Great Gatsby",
                Author = "F. Scott Fitzgerald",
                FileName = "great-gatsby.pdf",
                Price = 16,
                Description = "The story of the mysteriously wealthy Jay Gatsby and his love for the beautiful Daisy Buchanan.",
                Year = 1925,
                Pages = 180,
                CategoryId = 2,
                Language = LanguageType.English
            },
            new BookData
            {
                Name = "To Kill a Mockingbird",
                Author = "Harper Lee",
                FileName = "mockingbird.pdf",
                Price = 19,
                Description = "The unforgettable novel of a childhood in a sleepy Southern town and the crisis of conscience that rocked it.",
                Year = 1960,
                Pages = 324,
                CategoryId = 2,
                Language = LanguageType.English
            },
            new BookData
            {
                Name = "1984",
                Author = "George Orwell",
                FileName = "1984.pdf",
                Price = 17,
                Description = "A startling and haunting vision of the world, 1984 is so powerful that it is completely convincing from start to finish.",
                Year = 1949,
                Pages = 328,
                CategoryId = 2,
                Language = LanguageType.English
            },
            new BookData
            {
                Name = "Pride and Prejudice",
                Author = "Jane Austen",
                FileName = "pride-prejudice.pdf",
                Price = 15,
                Description = "Since its immediate success in 1813, Pride and Prejudice has remained one of the most popular novels in the English language.",
                Year = 1813,
                Pages = 432,
                CategoryId = 2,
                Language = LanguageType.English
            },
            new BookData
            {
                Name = "The Catcher in the Rye",
                Author = "J.D. Salinger",
                FileName = "catcher-rye.pdf",
                Price = 18,
                Description = "The hero-narrator of The Catcher in the Rye is an ancient child of sixteen, a native New Yorker named Holden Caulfield.",
                Year = 1951,
                Pages = 277,
                CategoryId = 2,
                Language = LanguageType.English
            },

            // Children Books
            new BookData
            {
                Name = "Harry Potter and the Sorcerer's Stone",
                Author = "J.K. Rowling",
                FileName = "harry-potter-1.pdf",
                Price = 25,
                Description = "Harry Potter has never been the star of a Quidditch team, scoring points while riding a broom far above the ground.",
                Year = 1997,
                Pages = 309,
                CategoryId = 3,
                Language = LanguageType.English
            },
            new BookData
            {
                Name = "The Chronicles of Narnia: The Lion, the Witch and the Wardrobe",
                Author = "C.S. Lewis",
                FileName = "narnia.pdf",
                Price = 20,
                Description = "Four adventurous siblings step through a wardrobe door and into the land of Narnia.",
                Year = 1950,
                Pages = 206,
                CategoryId = 3,
                Language = LanguageType.English
            },
            new BookData
            {
                Name = "Charlotte's Web",
                Author = "E.B. White",
                FileName = "charlottes-web.pdf",
                Price = 13,
                Description = "This beloved book by E. B. White, author of Stuart Little and The Trumpet of the Swan, is a classic of children's literature.",
                Year = 1952,
                Pages = 192,
                CategoryId = 3,
                Language = LanguageType.English
            },
            new BookData
            {
                Name = "Matilda",
                Author = "Roald Dahl",
                FileName = "matilda.pdf",
                Price = 17,
                Description = "Matilda is a sweet, exceptional young girl, but her parents think she's just a nuisance.",
                Year = 1988,
                Pages = 240,
                CategoryId = 3,
                Language = LanguageType.English
            },
            new BookData
            {
                Name = "Where the Wild Things Are",
                Author = "Maurice Sendak",
                FileName = "wild-things.pdf",
                Price = 19,
                Description = "Max, a wild and naughty boy, is sent to bed without his supper by his exhausted mother.",
                Year = 1963,
                Pages = 48,
                CategoryId = 3,
                Language = LanguageType.English
            },

            // Novels
            new BookData
            {
                Name = "The Lord of the Rings: The Fellowship of the Ring",
                Author = "J.R.R. Tolkien",
                FileName = "lotr-fellowship.pdf",
                Price = 23,
                Description = "In ancient times the Rings of Power were crafted by the Elven-smiths, and Sauron, the Dark Lord, forged the One Ring.",
                Year = 1954,
                Pages = 423,
                CategoryId = 4,
                Language = LanguageType.English
            },
            new BookData
            {
                Name = "The Hobbit",
                Author = "J.R.R. Tolkien",
                FileName = "hobbit.pdf",
                Price = 20,
                Description = "Bilbo Baggins is a hobbit who enjoys a comfortable, unambitious life, rarely traveling any farther than his pantry or cellar.",
                Year = 1937,
                Pages = 310,
                CategoryId = 4,
                Language = LanguageType.English
            },
            new BookData
            {
                Name = "Dune",
                Author = "Frank Herbert",
                FileName = "dune.pdf",
                Price = 22,
                Description = "Set on the desert planet Arrakis, Dune is the story of the boy Paul Atreides, heir to a noble family tasked with ruling an inhospitable world.",
                Year = 1965,
                Pages = 688,
                CategoryId = 4,
                Language = LanguageType.English
            },
            new BookData
            {
                Name = "The Name of the Wind",
                Author = "Patrick Rothfuss",
                FileName = "name-of-wind.pdf",
                Price = 25,
                Description = "Told in Kvothe's own voice, this is the tale of the magically gifted young man who grows to be the most notorious wizard his world has ever seen.",
                Year = 2007,
                Pages = 662,
                CategoryId = 4,
                Language = LanguageType.English
            },
            new BookData
            {
                Name = "The Way of Kings",
                Author = "Brandon Sanderson",
                FileName = "way-of-kings.pdf",
                Price = 28,
                Description = "Roshar is a world of stone and storms. Uncanny tempests of incredible power sweep across the rocky terrain.",
                Year = 2010,
                Pages = 1007,
                CategoryId = 4,
                Language = LanguageType.English
            }
        };

        await _context.Books.AddRangeAsync(books);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Seeded {Count} books", books.Count);
    }

    private async Task SeedOrdersAsync()
    {
        _logger.LogInformation("Seeding orders...");

        var users = await _context.Users.Where(u => u.Email!.StartsWith("user")).ToListAsync();
        var books = await _context.Books.ToListAsync();

        if (!users.Any() || !books.Any())
        {
            _logger.LogWarning("No users or books found. Skipping order seeding.");
            return;
        }

        var random = new Random(42); // Fixed seed for reproducibility
        var orders = new List<OrderData>();

        // Create orders for first 3 users
        for (int i = 0; i < Math.Min(3, users.Count); i++)
        {
            var user = users[i];
            var orderCount = i == 0 ? 5 : 3; // First user gets 5 orders, others get 3

            for (int j = 0; j < orderCount; j++)
            {
                var book = books[random.Next(books.Count)];
                var orderDate = DateTime.UtcNow.AddDays(-random.Next(1, 90));
                var quantity = random.Next(1, 4);

                var order = new OrderData
                {
                    UserId = user.Id,
                    BookId = book.Id,
                    Amount = quantity,
                    TimeCreated = orderDate,
                    State = j % 3 == 0 ? OrderState.New : j % 3 == 1 ? OrderState.Confirmed : OrderState.Confirmed,
                    Details = new OrderDataDetails
                    {
                        Book = new BookInfo
                        {
                            Id = book.Id,
                            Name = book.Name,
                            Author = book.Author,
                            Description = book.Description,
                            Price = book.Price,
                            Language = book.Language
                        }
                    }
                };

                orders.Add(order);
            }
        }

        await _context.Orders.AddRangeAsync(orders);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Seeded {Count} orders", orders.Count);
    }

    private async Task SeedCommentsAndRatingsAsync()
    {
        _logger.LogInformation("Seeding comments and ratings...");

        var users = await _context.Users.Where(u => u.Email!.StartsWith("user")).ToListAsync();
        var books = await _context.Books.Take(10).ToListAsync(); // Comment on first 10 books
        var orders = await _context.Orders.Include(o => o.Book).ToListAsync();

        if (!users.Any() || !books.Any())
        {
            _logger.LogWarning("No users or books found. Skipping comments and ratings seeding.");
            return;
        }

        var random = new Random(42);
        var comments = new List<CommentData>();
        var ratings = new List<RatingData>();

        var sampleComments = new[]
        {
            "Excellent book! Highly recommended.",
            "Great read, couldn't put it down.",
            "Very informative and well-written.",
            "A must-read for anyone interested in this topic.",
            "Loved every page of this book.",
            "Good book, but a bit slow in the middle.",
            "Interesting perspective on the subject.",
            "Well worth the read!",
            "One of the best books I've read this year.",
            "Engaging and thought-provoking."
        };

        // Add 2-3 comments per book
        foreach (var book in books)
        {
            var commentCount = random.Next(2, 4);
            for (int i = 0; i < commentCount && i < users.Count; i++)
            {
                var comment = new CommentData
                {
                    BookId = book.Id,
                    UserId = users[i].Id,
                    UserName = users[i].UserName ?? users[i].Email ?? "Anonymous",
                    Note = sampleComments[random.Next(sampleComments.Length)],
                    TimeCreated = DateTime.UtcNow.AddDays(-random.Next(1, 60))
                };
                comments.Add(comment);
            }
        }

        // Add ratings for completed orders
        foreach (var order in orders.Where(o => o.State == OrderState.Confirmed))
        {
            var rating = new RatingData
            {
                OrderId = order.Id,
                BookId = order.BookId,
                Score = (RatingScore)random.Next(3, 6), // 3-5 stars
                TimeCreated = order.TimeCreated.AddDays(random.Next(1, 7))
            };
            ratings.Add(rating);
        }

        await _context.Comments.AddRangeAsync(comments);
        await _context.Set<RatingData>().AddRangeAsync(ratings);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Seeded {CommentCount} comments and {RatingCount} ratings",
            comments.Count, ratings.Count);
    }
}
