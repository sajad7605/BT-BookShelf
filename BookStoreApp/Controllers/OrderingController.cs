
    namespace BookStoreApp.Controllers
{   
    [Route("api/[controller]")]
    public class OrderingController : Controller
    {
        private readonly IRepository<Author> _AuthorRepo;
        private readonly IRepository<Ordering> _OrderingRepo;
        private readonly IMapper _Mapper;
        public OrderingController(IRepository<Author> AuthorRepo, IMapper mapper, IRepository<Ordering> OrderingRepo)
        {
            _AuthorRepo = AuthorRepo;
            _Mapper = mapper;
            _OrderingRepo = OrderingRepo;
        }



        [HttpGet]
        [Route("")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Ordering>))]
        public async Task<IActionResult> RetrieveAllOrderings()
        {
            IEnumerable<Ordering> Orderings = await _OrderingRepo.GetAllAsync();
            IEnumerable<OrderingDto> Final = _Mapper.Map<IEnumerable<OrderingDto>>(Orderings);
            return Ok(Final);
        }



        [HttpGet]
        [Route("{Orderingid}")]
        [ProducesResponseType(200, Type = typeof(OrderingDto))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RetrieveASpecificOrderingByID(int Orderingid)
        {
            var Ordering = await _OrderingRepo.GetByIdAsync(Orderingid);
            if (Ordering is null)
            {
                return NotFound("No Ordering exists with the provided id");
            }
            var Final = _Mapper.Map<OrderingDto>(Ordering);
            return Ok(Final);
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> CreateANewOrdering([FromBody] OrderingDto OrderingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _OrderingRepo.GetByNameAsync(OrderingDto.OrderName) is not null)
            {
                return BadRequest("Sorry the Ordering Already exists");
            }

            Ordering Final = _Mapper.Map<Ordering>(OrderingDto);

            try
            {
                if (!await _OrderingRepo.AddAsync(Final))
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
        [Route("{Orderingid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateAnExistingAuthor(int Orderingid,[FromBody] OrderingDto OrderingDto){
            if (Orderingid != OrderingDto.Id){
                return BadRequest("IDs don't match!");
            }
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            Console.WriteLine("LINE Error 85");
            if(await _OrderingRepo.GetByIdAsync(Orderingid) is null){
                return NotFound($"No Ordering Exists with the provided Id {Orderingid}");
            }
            Console.WriteLine("LINE Error 89");
            Ordering Final= _Mapper.Map<Ordering>(OrderingDto);
            if (!await _OrderingRepo.UpdateAsync(Final)){
                return BadRequest("sth went wrong updating the Ordering");
            }
            return NoContent();
        }


        [HttpDelete]
        [Route("{Orderingid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteAnAuthor(int Orderingid){
            if (await _OrderingRepo.GetByIdAsync(Orderingid) is null){
                return BadRequest($"No User Exists with the provided id {Orderingid} to delete");
            }
            if(!await _OrderingRepo.DeleteByIdAsync(Orderingid)){
                return BadRequest("Sth went wrong while deleting the Author");
            }
            return NoContent();
        }


    }
}
