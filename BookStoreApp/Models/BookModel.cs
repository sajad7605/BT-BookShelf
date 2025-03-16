
namespace BookStoreApp{
    public class Book{
        [Key]
        public int Id{get;set;}
        public string Name { get; set; }
        public ICollection<BookAuthor>? BookAuthors{get;set;}
        public ICollection<BookCategory>? BookCategories{get;set;}
        public ICollection<BookOrdering>? BookOrders{get;set;}
    }
}