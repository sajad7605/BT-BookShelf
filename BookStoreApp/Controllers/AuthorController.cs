using BookStoreApp.Dtos;
using BookStoreApp.Interfaces;

namespace BookStoreApp.Controllers
{

    [Route("api/[controller]")]
    public class AuthorsController : Controller
    {
        private readonly IRepository<Author> _AuthorRepo;
        private readonly IMapper _Mapper;
        public AuthorsController(IRepository<Author> AuthorRepo, IMapper mapper)
        {
            _AuthorRepo = AuthorRepo;
            _Mapper = mapper;
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


    }
}