namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IRepository<User> _UserRepo;

        private readonly UserManager<User> _UserManager;
        private readonly SignInManager<User> _SignInManager;
        private readonly RoleManager<IdentityRole> _RoleManager;
        private readonly AppDbContext _Context;
        public UserController(IRepository<User> UserRepo, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, AppDbContext context)
        {
            _UserRepo = UserRepo;
            _UserManager = userManager;
            _Context = context;
            _SignInManager = signInManager;
            _RoleManager = roleManager;

        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RetrieveAllUsers()
        {
            IEnumerable<User>? users = await _UserManager.Users.ToListAsync();
            if (users is null)
            {
                return NotFound("No User Exists");
            }
            return Ok(users);
        }

        [HttpGet]
        [Route("{Userid}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RetrieveASpecificUserByID(string Userid)
        {
            User? user = await _UserManager.FindByIdAsync(Userid);
            if (user is null)
            {
                return NotFound($"User doesn't exist with the provided id {Userid}");
            }
            return Ok(user);

        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> RegisterANewUser([FromBody] UserRegisterDto userDto)
        {
            using (_Context.Database.BeginTransaction())
            {
                try{
                    if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                User? user = await _UserManager.FindByEmailAsync(userDto.Email);
                if (user is not null)
                {
                    return BadRequest("sorry a user exists with the provided email");
                }
                User NewUser = new User
                {
                    Email = userDto.Email,
                    UserName = userDto.Email
                };
                var Creationresult = await _UserManager.CreateAsync(NewUser, userDto.Password);
                if (Creationresult.Succeeded)
                {
                    IdentityResult Roleresult = await _UserManager.AddToRoleAsync(NewUser, Roles.user);
                }
                _Context.SaveChanges();
                _Context.Database.CommitTransaction();
                return NoContent();
                }catch{
                    _Context.Database.RollbackTransaction();
                    return BadRequest("sth went wrong saving the user!");
                }
                
            }

        }


        [HttpPost]
        [Route("")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userDto){
             if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            User? User = await _UserManager.FindByEmailAsync(userDto.Email);
            if (User is not null)
            {
                bool passwordCheck = await _UserManager.CheckPasswordAsync(User, userDto.Password);
                if (passwordCheck)
                {
                    var result = await _SignInManager.PasswordSignInAsync(User, userDto.Password, true, true);
                    if (result.Succeeded)
                    {
                       return NoContent();
                    }
                }
                return BadRequest("Wrong Credentials");

            }
            return NotFound("User doesn't exist!");
        }
    }
}