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

        [HttpGet]
        [Route("{Bookid}")]
        [ProducesResponseType(200,Type = typeof(BookDto))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RetrieveASpecificBookByID(int Bookid){
            var book=await _BookRepo.GetByIdAsync(Bookid);
            if (book is null){
                return NotFound("No book exists with the provided id");
            }
            var Final=_Mapper.Map<BookDto>(book);
            return Ok(Final);
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> CreateANewBook([FromBody] BookDto bookDto){
            if (!ModelState.IsValid){
                return BadRequest("Modelstate is not vaid ");
            }
            if (await _BookRepo.GetByIdAsync(bookDto.Id) is not null){
                return BadRequest("Sorry the Book Already exists");
            }

            Book Final=_Mapper.Map<Book>(bookDto);
            if (!await _BookRepo.AddAsync(Final)){
                return BadRequest("sth went wrong saving the customer");
            }
            return NoContent();
        }
        
    }
}