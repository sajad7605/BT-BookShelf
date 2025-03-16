namespace BookStoreApp.Dtos{
    public class CategoryDto{
        public int Id{get;set;}
         [Required(ErrorMessage = "You must enter the category's name")]
        [Length(5,100, ErrorMessage = "enter a valid category name (between 5 and 100 letters)")]
        public string CategoryName { get; set; }
    }
}