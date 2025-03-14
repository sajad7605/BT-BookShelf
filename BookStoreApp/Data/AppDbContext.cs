using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
namespace BookStoreApp.Data{
    public class AppDbContext:IdentityDbContext<User>{
        private readonly IOptions<ConnectionStringModel> _ConnectionString;

        public AppDbContext(DbContextOptions<AppDbContext> options,IOptions<ConnectionStringModel> ConnectionString): base(options){
            _ConnectionString=ConnectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_ConnectionString.Value.DefaultConnection);
            base.OnConfiguring(optionsBuilder);
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<BookAuthor>().HasKey(key=>new {key.BookId,key.AuthorId});
            builder.Entity<BookAuthor>().HasOne(b=>b.book).WithMany(ba=>ba.BookAuthors).HasForeignKey(f=>f.BookId);
            builder.Entity<BookAuthor>().HasOne(b=>b.author).WithMany(ba=>ba.AuthorBooks).HasForeignKey(f=>f.AuthorId);


            builder.Entity<BookCategory>().HasKey(key=> new {key.BookId,key.CategoryId});
            builder.Entity<BookCategory>().HasOne(b=>b.book).WithMany(bc=>bc.BookCategories).HasForeignKey(f=>f.BookId);
            builder.Entity<BookCategory>().HasOne(c=>c.category).WithMany(cof=>cof.FullCategorybook).HasForeignKey(f=>f.CategoryId);


            builder.Entity<BookOrdering>().HasKey(key=>new {key.BookId,key.OrderId});
            builder.Entity<BookOrdering>().HasOne(b=>b.book).WithMany(bo=>bo.BookOrders).HasForeignKey(f=>f.BookId);
            builder.Entity<BookOrdering>().HasOne(o=>o.ordering).WithMany(b=>b.BooksInOneOrder).HasForeignKey(f=>f.OrderId);

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books{get;set;}
        public DbSet<Ordering> Orderings { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<BookCategory> BookCategories  { get; set; }
        public DbSet<BookOrdering> BookOrderings { get; set; }


    }
}