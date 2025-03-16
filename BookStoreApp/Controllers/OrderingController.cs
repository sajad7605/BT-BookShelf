
namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly IRepository<Author> _AuthorRepo;
        private readonly IRepository<Category> _CategoryRepo;
        private readonly IMapper _Mapper;
        public CategoryController(IRepository<Author> AuthorRepo, IMapper mapper, IRepository<Category> CategoryRepo)
        {
            _AuthorRepo = AuthorRepo;
            _Mapper = mapper;
            _CategoryRepo = CategoryRepo;
        }



        [HttpGet]
        [Route("")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public async Task<IActionResult> RetrieveAllCategorys()
        {
            IEnumerable<Category> Categorys = await _CategoryRepo.GetAllAsync();
            IEnumerable<CategoryDto> Final = _Mapper.Map<IEnumerable<CategoryDto>>(Categorys);
            return Ok(Final);
        }



        [HttpGet]
        [Route("{Categoryid}")]
        [ProducesResponseType(200, Type = typeof(CategoryDto))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RetrieveASpecificCategoryByID(int Categoryid)
        {
            var Category = await _CategoryRepo.GetByIdAsync(Categoryid);
            if (Category is null)
            {
                return NotFound("No Category exists with the provided id");
            }
            var Final = _Mapper.Map<CategoryDto>(Category);
            return Ok(Final);
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> CreateANewCategory([FromBody] CategoryDto CategoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Modelstate is not vaid ");
            }
            if (await _CategoryRepo.GetByIdAsync(CategoryDto.Id) is not null)
            {
                return BadRequest("Sorry the Category Already exists");
            }

            Category Final = _Mapper.Map<Category>(CategoryDto);

            try
            {
                if (!await _CategoryRepo.AddAsync(Final))
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
        [Route("{Categoryid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateAnExistingAuthor(int Categoryid, [FromBody] CategoryDto CategoryDto)
        {
            if (Categoryid != CategoryDto.Id)
            {
                return BadRequest("IDs don't match!");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Console.WriteLine("LINE Error 85");
            if (await _CategoryRepo.GetByIdAsync(Categoryid) is null)
            {
                return NotFound($"No Category Exists with the provided Id {Categoryid}");
            }
            Console.WriteLine("LINE Error 89");
            Category Final = _Mapper.Map<Category>(CategoryDto);
            if (!await _CategoryRepo.UpdateAsync(Final))
            {
                return BadRequest("sth went wrong updating the Category");
            }
            return NoContent();
        }


        [HttpDelete]
        [Route("{Categoryid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteAnAuthor(int Categoryid)
        {
            if (await _CategoryRepo.GetByIdAsync(Categoryid) is null)
            {
                return BadRequest($"No User Exists with the provided id {Categoryid} to delete");
            }
            if (!await _CategoryRepo.DeleteByIdAsync(Categoryid))
            {
                return BadRequest("Sth went wrong while deleting the Author");
            }
            return NoContent();
        }


    }
}
