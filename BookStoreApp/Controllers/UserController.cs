namespace BookStoreApp.Controllers{
    [Route("api/[controller]")]
    public class UserController:Controller{
        private readonly IRepository<User> _UserRepo;

        private readonly UserManager<User> _UserManager;
        private readonly SignInManager<User> _SignInManager;
        private readonly RoleManager<IdentityRole> _RoleManager;
        public UserController(IRepository<User> UserRepo,UserManager<User> userManager,SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _UserRepo=UserRepo;
            _UserManager=userManager;
            _SignInManager=signInManager;
            _RoleManager=roleManager;
            
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(200,Type = typeof(IEnumerable<User>))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RetrieveAllUsers(){
            IEnumerable<User>? users=await _UserManager.Users.ToListAsync();
            if (users is null){
                return NotFound("No User Exists");
            }
            return Ok(users);
        }

        [HttpGet]
        [Route("{Userid}")]
        [ProducesResponseType(200,Type=typeof(User))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RetrieveASpecificUserByID(string Userid){
            User? user=await _UserManager.FindByIdAsync(Userid);
            if (user is null){
                 return NotFound($"User doesn't exist with the provided id {Userid}");
            }
            return Ok(user);

        }
        
        [HttpPost]
        [Route("")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> RegisterANewUser([FromBody] UserDto userDto){
            if(!ModelState.IsValid){
                return BadRequest("ModelState Is Not Valid");
            }
            User? user=await _UserManager.FindByEmailAsync(userDto.Email);
            if (user is not null){
                return BadRequest("sorry a user exists with the provided email");
            }
            User NewUser= new User{
                Email=userDto.Email,
                UserName=userDto.Email
            };
            var Creationresult=await _UserManager.CreateAsync(NewUser,userDto.Password);
            if(Creationresult.Succeeded){
                 IdentityResult Roleresult = await _UserManager.AddToRoleAsync(NewUser,Roles.user);
            }

        }
    }
}