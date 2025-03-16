namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        private readonly IRepository<Author> _AuthorRepo;
        private readonly IRepository<Book> _BookRepo;
        private readonly IMapper _Mapper;
        public BookController(IRepository<Author> AuthorRepo, IMapper mapper, IRepository<Book> BookRepo)
        {
            _AuthorRepo = AuthorRepo;
            _Mapper = mapper;
            _BookRepo = BookRepo;
        }



        [HttpGet]
        [Route("")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Book>))]
        public async Task<IActionResult> RetrieveAllBooks()
        {
            IEnumerable<Book> books = await _BookRepo.GetAllAsync();
            IEnumerable<BookDto> Final = _Mapper.Map<IEnumerable<BookDto>>(books);
            return Ok(Final);
        }



        [HttpGet]
        [Route("{Bookid}")]
        [ProducesResponseType(200, Type = typeof(BookDto))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RetrieveASpecificBookByID(int Bookid)
        {
            var book = await _BookRepo.GetByIdAsync(Bookid);
            if (book is null)
            {
                return NotFound("No book exists with the provided id");
            }
            var Final = _Mapper.Map<BookDto>(book);
            return Ok(Final);
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> CreateANewBook([FromBody] BookDto bookDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _BookRepo.GetByNameAsync(bookDto.BookName) is not null)
            {
                return BadRequest("Sorry the Book Already exists");
            }

            Book Final = _Mapper.Map<Book>(bookDto);

            try
            {
                if (!await _BookRepo.AddAsync(Final))
                {
                    return BadRequest("sth went wrong saving the customer");
                }
            }
            catch
            {
                return BadRequest("just remove the id from body, you can't have id in your body for httppost, it raises a freaking error from sql saying id_isertion is set to off");
            }
            return NoContent();
        }
        [HttpPut]
        [Route("{Bookid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateAnExistingAuthor(int Bookid,[FromBody] BookDto bookDto){
            if (Bookid != bookDto.Id){
                return BadRequest("IDs don't match!");
            }
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            Console.WriteLine("LINE Error 85");
            if(await _BookRepo.GetByIdAsync(Bookid) is null){
                return NotFound($"No Book Exists with the provided Id {Bookid}");
            }
            Console.WriteLine("LINE Error 89");
            Book Final= _Mapper.Map<Book>(bookDto);
            if (!await _BookRepo.UpdateAsync(Final)){
                return BadRequest("sth went wrong updating the Book");
            }
            return NoContent();
        }


        [HttpDelete]
        [Route("{Bookid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteAnAuthor(int Bookid){
            if (await _BookRepo.GetByIdAsync(Bookid) is null){
                return BadRequest($"No User Exists with the provided id {Bookid} to delete");
            }
            if(!await _BookRepo.DeleteByIdAsync(Bookid)){
                return BadRequest("Sth went wrong while deleting the Author");
            }
            return NoContent();
        }


    }
}