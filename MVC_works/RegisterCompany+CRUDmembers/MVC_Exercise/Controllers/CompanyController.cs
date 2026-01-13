using Microsoft.AspNetCore.Mvc;
using MVC_Exercise.Models.DTOs;
using MVC_Exercise.Services;

namespace MVC_Exercise.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyService _service;

        public CompanyController(ICompanyService service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(CompanyDto companyDto)
        {
            try
            {
                var success = _service.CompanyRegister(companyDto);
                if (success)
                {
                    return RedirectToAction("Login");
                }
                ViewBag.ErrorMessage = "Registration failed. Company Already exist.";
                return View(companyDto);
            }
           catch
            {
                return View();
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginDto loginDto)
        {
            var result=_service.LoginCompany(loginDto.Email,loginDto.Password); 
              if(result != null)
            {
                HttpContext.Session.SetString("CompanyId", result.CompanyId.ToString());
                HttpContext.Session.SetString("CompanyName",result.CompanyName);
                return RedirectToAction("Dashboard");
            }
            else
            {
                ViewBag.Message = "Invalid Credentials";
                return View();
            }
        }

        public IActionResult Dashboard()
        {
            var WelcomeMessage = HttpContext.Session.GetString("CompanyName");
            var getCompanyid = HttpContext.Session.GetString("CompanyId");

            if(string.IsNullOrEmpty(getCompanyid))
            {
                return RedirectToAction("Login");
            }
            Guid companyId=new Guid(getCompanyid);

            var companyDetails=_service.GetCompanyById(companyId);
            if(companyDetails != null)
            {
                ViewBag.WelcomeMessage = WelcomeMessage;
                return View(companyDetails);
            }

            return View();


        }
    }
}
