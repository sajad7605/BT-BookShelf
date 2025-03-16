namespace BookStoreApp.Dtos{
    public class AuthorDto{
        public int Id{get;set;}
        [Required(ErrorMessage = "You must enter the authors fullname")]
        [Length(5,100, ErrorMessage = "enter a valid full name (between 5 and 100 letters)")]
        public string FullName { get; set; }
    }
}