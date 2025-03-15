using BookStoreApp.Dtos;
using BookStoreApp.Interfaces;

namespace BookStoreApp.Controllers
{

    [Route("api/[controller]")]
    public class AuthorsController : Controller
    {
        private readonly IRepository<Author> _AuthorRepo;
        private readonly IRepository<Book> _BookRepo;
        private readonly IMapper _Mapper;
        public AuthorsController(IRepository<Author> AuthorRepo, IMapper mapper,IRepository<Book> BookRepo)
        {
            _AuthorRepo = AuthorRepo;
            _Mapper = mapper;
            _BookRepo=BookRepo;
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AuthorDto>))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RetrieveAllAuthors()
        {
            try
            {
                IEnumerable<Author> Authors = await _AuthorRepo.GetAllAsync();
                IEnumerable<AuthorDto> Final = _Mapper.Map<IEnumerable<AuthorDto>>(Authors);
                return Ok(Final);
            }
            catch (System.Exception)
            {
                return NotFound("No User Exists");
            }
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(200, Type = typeof(AuthorDto))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RetrieveASpecificAuthorByID(int id)
        {
            Author? author = await _AuthorRepo.GetByIdAsync(id);
            if (author is null)
            {
                return NotFound("There was no user with the provided id");
            }
            AuthorDto Final = _Mapper.Map<AuthorDto>(author);
            return Ok(Final);

        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateANewAuthor([FromBody] AuthorDto authorDto,[FromQuery] IEnumerable<BookDto> bookDtos,[FromQuery] IEnumerable<int> bookIds){

            

            if(!ModelState.IsValid){
                return BadRequest("Modelstate is not valid");
            }
            var author=await _AuthorRepo.GetByIdAsync(authorDto.Id);
            if(author is not null){
                return BadRequest($"sorry an author with the given id ({authorDto.Id}) already exists!");
            }
            Author Final= _Mapper.Map<Author>(authorDto);
            IEnumerable<Book> BooksWrittenByAuthor=_Mapper.Map<IEnumerable<Book>>(bookDtos);
            IEnumerable<Book> BooksWroteByAuthor=await _BookRepo.GetRangeByIdsAsync(bookIds);
            
            try{
                if (!await _AuthorRepo.AddAsync(Final)){
                
                return BadRequest("an error occured while inserting the Author");
            }
            }catch{
                return BadRequest("just remove the id from body, you can't have id in your body for httppost, it raises a freaking error from sql saying id_isertion is set to off");
            }

            
            return NoContent();
 
        }


        [HttpPut]
        [Route("{Authorid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateAnExistingAuthor(int Authorid,[FromBody] AuthorDto authorDto){
            if (Authorid != authorDto.Id){
                return BadRequest("IDs don't match!");
            }
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            Console.WriteLine("LINE Error 85");
            if(await _AuthorRepo.GetByIdAsync(Authorid) is null){
                return NotFound($"No User Exists with the provided Id {Authorid}");
            }
            Console.WriteLine("LINE Error 89");
            Author Final= _Mapper.Map<Author>(authorDto);
            if (!await _AuthorRepo.UpdateAsync(Final)){
                return BadRequest("sth went wrong updating the user");
            }
            return NoContent();
        }

        [HttpDelete]
        [Route("{Authorid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteAnAuthor(int Authorid){
            if (await _AuthorRepo.GetByIdAsync(Authorid) is null){
                return BadRequest($"No User Exists with the provided id {Authorid} to delete");
            }
            if(!await _AuthorRepo.DeleteByIdAsync(Authorid)){
                return BadRequest("Sth went wrong while deleting the Author");
            }
            return NoContent();
        }

    }
}