namespace BookStoreApp.Models{
    public class Ordering{
        [Key]
        public int Id { get; set; }
        public DateTime OrderDateTime { get; set; }
        public ICollection<BookOrdering> BooksInOneOrder{get;set;}
    }
}