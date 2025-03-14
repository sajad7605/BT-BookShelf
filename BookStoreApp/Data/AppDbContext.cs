using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;
namespace BookStoreApp.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        private readonly IOptions<ConnectionStringModel> _ConnectionString;

        public AppDbContext(DbContextOptions<AppDbContext> options, IOptions<ConnectionStringModel> ConnectionString) : base(options)
        {
            _ConnectionString = ConnectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_ConnectionString.Value.DefaultConnection);
            base.OnConfiguring(optionsBuilder);


        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<BookAuthor>().HasKey(key => new { key.BookId, key.AuthorId });
            builder.Entity<BookAuthor>().HasOne(b => b.book).WithMany(ba => ba.BookAuthors).HasForeignKey(f => f.BookId);
            builder.Entity<BookAuthor>().HasOne(b => b.author).WithMany(ba => ba.AuthorBooks).HasForeignKey(f => f.AuthorId);


            builder.Entity<BookCategory>().HasKey(key => new { key.BookId, key.CategoryId });
            builder.Entity<BookCategory>().HasOne(b => b.book).WithMany(bc => bc.BookCategories).HasForeignKey(f => f.BookId);
            builder.Entity<BookCategory>().HasOne(c => c.category).WithMany(cof => cof.FullCategorybook).HasForeignKey(f => f.CategoryId);


            builder.Entity<BookOrdering>().HasKey(key => new { key.BookId, key.OrderId });
            builder.Entity<BookOrdering>().HasOne(b => b.book).WithMany(bo => bo.BookOrders).HasForeignKey(f => f.BookId);
            builder.Entity<BookOrdering>().HasOne(o => o.ordering).WithMany(b => b.BooksInOneOrder).HasForeignKey(f => f.OrderId);


            builder.Entity<Book>().HasData(
                new Book { Id = 1, BookName = "Pride and Prejudice" },
                new Book { Id = 2, BookName = "A Man Called Over" },
                new Book { Id = 3, BookName = "Harry Potter 1" },
                new Book { Id = 4, BookName = "Harry Potter 2" },
                new Book { Id = 5, BookName = "Harry Potter 3" },
                new Book { Id = 6, BookName = "Harry Potter 4" }
            );

            builder.Entity<Author>().HasData(
                new Author { Id = 1, FullName = "Jane Austin" }
                , new Author { Id = 2, FullName = "JK rowling" }
            );

            builder.Entity<Ordering>().HasData(
                new Ordering { Id = 1, OrderDateTime = new DateTime(2025, 3, 14) }
                , new Ordering { Id = 2, OrderDateTime = new DateTime(2025, 3, 14) }
            );
            builder.Entity<Category>().HasData(
                new Category { Id = 1, CategoryType = "Horror" },
                new Category { Id = 2, CategoryType = "Romance" }
            );
            builder.Entity<BookAuthor>().HasData(
                new BookAuthor { BookId = 1, AuthorId = 2 },
                new BookAuthor { BookId = 3, AuthorId = 2 },
                new BookAuthor { BookId = 4, AuthorId = 2 },
                new BookAuthor { BookId = 2, AuthorId = 1 },
                 new BookAuthor { BookId = 4, AuthorId = 1 }
            );
            builder.Entity<BookCategory>().HasData(
                new BookCategory { BookId = 1, CategoryId = 2 },
                new BookCategory { BookId = 2, CategoryId = 1 },
                new BookCategory { BookId = 3, CategoryId = 2 },
                new BookCategory { BookId = 5, CategoryId = 1 },
                new BookCategory { BookId = 5, CategoryId = 2 },
                new BookCategory {BookId=3,CategoryId=1}
            );
            builder.Entity<BookOrdering>().HasData(
                new BookOrdering { BookId = 1, OrderId = 2 },
                new BookOrdering { BookId = 1, OrderId = 1 },
                new BookOrdering { BookId = 2, OrderId = 2 },
                new BookOrdering { BookId = 2, OrderId = 1},
                new BookOrdering { BookId = 4, OrderId = 2 },
                new BookOrdering { BookId = 5, OrderId = 2 },
                new BookOrdering { BookId = 6, OrderId = 1 },
                new BookOrdering { BookId = 6, OrderId = 2 }
            );

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Ordering> Orderings { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<BookOrdering> BookOrderings { get; set; }


    }
}