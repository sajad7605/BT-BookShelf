namespace BookStoreApp.Dtos{
    public class BookDto{
        public int Id{get;set;}
         [Required(ErrorMessage = "You must enter the books name")]
        [Length(5,100, ErrorMessage = "enter a valid bookname (between 5 and 100 letters)")]
        public string BookName { get; set; }
    }
}