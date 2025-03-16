namespace BookStoreApp.Dtos{
    public class UserRegisterDto{
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [Required(ErrorMessage = "Email Address is required")]
        public string Email { get; set; }

        [PasswordPropertyText]
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Password is required")]
        public string Password { get; set; }

        [Compare("Password",ErrorMessage = "Passwords do not match")]
        [Required(ErrorMessage = "You must confirm your password")]
        public string RepeatPassword { get; set; }
    }
}