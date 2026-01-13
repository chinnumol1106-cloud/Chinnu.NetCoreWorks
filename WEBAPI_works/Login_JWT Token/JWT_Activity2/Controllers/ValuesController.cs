using JWT_Activity2.Interface;
using JWT_Activity2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWT_Activity2.Controllers
{
    [Route("api/Values")]
    [ApiController]
    [Authorize(Roles = "SEEKER")]
    public class ValuesController : ControllerBase
    {
        AppDbContext _context;
        ITokenInterface _token;

        public ValuesController(AppDbContext context, ITokenInterface token)
        {
            _context = context;
            _token = token;
        }

        [HttpGet("/Users")]
        public ActionResult<List<User>> GetUser()
        {
            var users = _context.Users.ToList();
            return Ok(users);
        }

        [HttpPost("/register")]
        [AllowAnonymous]
        public IActionResult Register(User user)
        {
            user.Id = Guid.NewGuid();
            user.role = Enums.Role.SEEKER;
            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok(user);
        }

        [HttpPost("/login")]
        [AllowAnonymous]
        public IActionResult Login(LoginRequest loginuser)
        
        {
            var existuser=_context.Users.FirstOrDefault(u=>u.Email == loginuser.Email && u.Password == loginuser.Password);
            if(existuser == null)
            {
                return NotFound();
            }

            else
            {
                string Token=_token.CreateToken(existuser);
                return Ok(Token);
            }


        }


        [HttpGet("/GetUserName")]
        public ActionResult GetName()
        {
            var username = _token.GetuserName();
            return Ok(username);
        }
    }
}
