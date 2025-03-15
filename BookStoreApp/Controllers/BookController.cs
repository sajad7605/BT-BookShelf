namespace BookStoreApp.Controllers{
    [Route("api/[controller]")]
    public class BookController:Controller{
        private readonly IRepository<Author> _AuthorRepo;
        private readonly IRepository<Book> _BookRepo;
        private readonly IMapper _Mapper;
         public BookController(IRepository<Author> AuthorRepo, IMapper mapper,IRepository<Book> BookRepo)
        {
            _AuthorRepo = AuthorRepo;
            _Mapper = mapper;
            _BookRepo=BookRepo;
        }
        [HttpGet]
        [Route("")]
        [ProducesResponseType(200,Type = typeof (IEnumerable<Book>))]
        public async Task<IActionResult> RetrieveAllBooks(){
            var books=_BookRepo.GetAllAsync();
            IEnumerable<BookDto> Final= _Mapper.Map<IEnumerable<BookDto>>(books);
            return Ok(Final);
        }

    }
}