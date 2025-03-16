namespace BookStoreApp.Dtos{
    public class OrderingDto{

        public int Id { get; set; }
        [Required(ErrorMessage = "You must enter the Order's name")]
        [Length(5,100, ErrorMessage = "enter a valid order name (between 5 and 100 letters)")]
        public string OrderName { get; set; }
        public DateTime OrderDateTime { get; set; }

    }
}