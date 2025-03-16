namespace BookStoreApp.Models{
    public class Author{
        [Key]
        public int Id{get;set;}
        public string Name { get; set; }
        public ICollection<BookAuthor>? AuthorBooks { get; set; }
    }
}