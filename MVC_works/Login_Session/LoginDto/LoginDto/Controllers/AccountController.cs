using AutoMapper;
using LoginDto.DTOs;
using LoginDto.Models;
using Microsoft.AspNetCore.Mvc;

namespace LoginDto.Controllers
{
    public class AccountController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AccountController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserDTO userdto)
        {
            if(ModelState.IsValid)
            {
                var newuser=_mapper.Map<User>(userdto);
                _context.Users.Add(newuser);
                _context.SaveChanges();
                ViewBag.Message = "Registration Successfully";
                return RedirectToAction("Login");

            }
             return View();
        }

        //Login

        public IActionResult Login()=>View();

        [HttpPost]
        public IActionResult Login(UserDTO userDto)
        {
            var existuser= _context.Users.FirstOrDefault(u=>u.username==userDto.username&&u.Password==userDto.Password);

            if(existuser!=null)
            {
                HttpContext.Session.SetInt32("UserId",existuser.Id);
                HttpContext.Session.SetString("UserName", existuser.username);
                return RedirectToAction("Dashboard");
            }

            ModelState.AddModelError("","Invalid Login Credentials");
            return View();
        }


        //Dashboard

        public IActionResult Dashboard()
        {
            var loginuser = HttpContext.Session.GetString("UserName");

            if(loginuser!=null)
            {
                ViewBag.loguser=loginuser;
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        //Logout

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View();
        }

        

        //Profile

        public IActionResult Profile()
        {
            var userid = HttpContext.Session.GetInt32("UserId");
            if (userid == null)
                return RedirectToAction("Login");

            var userprofile= _context.Users.FirstOrDefault(u=>u.Id==userid);
            var userprofiledto=_mapper.Map<UserDTO>(userprofile);
            return View(userprofiledto);
        }

        //Edit Profile


        public IActionResult EditProfile(int id)
        {
           var edituser= _context.Users.FirstOrDefault(u=>u.Id == id);
            if (edituser == null)
                return RedirectToAction("Login");

            var edituserDto=_mapper.Map<UserDTO>(edituser);
            return View(edituserDto);
        }


        [HttpPost]
        public IActionResult EditProfile(UserDTO user)
        {
            var exist= _context.Users.FirstOrDefault(u=>u.Id==user.Id);
            if (exist == null)
                return RedirectToAction("Login");

            _mapper.Map(user, exist);
            _context.SaveChanges();
            return RedirectToAction("Profile");
        }
    }
}

