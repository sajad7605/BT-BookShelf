namespace BookStoreApp.Models{
    public class Category{
        public int Id{get;set;}
        public string CategoryType { get; set; }
        public ICollection<Book>? BookCategories { get; set; }
    }
}