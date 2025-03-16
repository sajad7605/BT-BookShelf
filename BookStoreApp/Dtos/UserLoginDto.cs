namespace BookStoreApp.Dtos{
    public class UserLoginDto{
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [Required(ErrorMessage = "Email Address is required")]
        public string Email { get; set; }

        [PasswordPropertyText]
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Password is required")]
        public string Password { get; set; }
    }
}