namespace BookStoreApp.Models{
    public class BookOrdering{
        public int BookId{get;set;}
        public int OrderId{get;set;}
        public Book book{get;set;}
        public Ordering ordering{get;set;}
    }
}