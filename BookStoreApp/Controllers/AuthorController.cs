using BookStoreApp.Dtos;
using BookStoreApp.Interfaces;

namespace BookStoreApp.Controllers{

    [Route("api/[controller]")]
    public class AuthorsController:Controller{
        private readonly IRepository<Author> _AuthorRepo;
        private readonly IMapper _Mapper;
        public AuthorsController(IRepository<Author> AuthorRepo,IMapper mapper)
        {
            _AuthorRepo=AuthorRepo;
            _Mapper=mapper;
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(200, Type =typeof(IEnumerable<AuthorDto>))]
        public async Task<IActionResult> Retrieveallauthors(){
            IEnumerable<Author> Authors= await _AuthorRepo.GetAllAsync();
            IEnumerable<AuthorDto> Final= _Mapper.Map<IEnumerable<AuthorDto>>(Authors);
            return Ok(Final);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(200,Type = typeof(AuthorDto))]

        
    }
}