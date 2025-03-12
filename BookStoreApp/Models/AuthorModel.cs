namespace BookStoreApp.Models{
    public class Author{
        [Key]
        public int Id{get;set;}
        public string FullName { get; set; }
        public ICollection<Book>? Books { get; set; }
    }
}