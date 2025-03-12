
namespace BookStoreApp{
    public class Book{
        [Key]
        public int Id{get;set;}
        public string BookName { get; set; }
        public ICollection<Author>? BookAuthors{get;set;}
        public ICollection<Category>? BookCategories{get;set;}
        public ICollection<Ordering>? BookOrderings{get;set;}
    }
}