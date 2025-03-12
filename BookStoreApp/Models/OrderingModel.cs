namespace BookStoreApp.Models{
    public class Ordering{
        [Key]
        public int Id { get; set; }
        public ICollection<Book> Books{get;set;}
    }
}