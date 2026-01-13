using Microsoft.AspNetCore.Mvc;
using MVC_Exercise.Models.DTOs;
using MVC_Exercise.Services;

namespace MVC_Exercise.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userservice;

        public UserController(IUserService userservice)
        {
            _userservice = userservice;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserRegister()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UserRegister(MemberDto newMember)

        {
          var success=_userservice.AddMember(newMember);
            if(success)
            {
               return RedirectToAction("Dashboard","Company");
            }
            return View();

        }

        public IActionResult RemoveMember()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RemoveMember(MemberDto removeMember)
        {
         var success=_userservice.DeleteMemberByDetails(removeMember.CompanyId.Value,removeMember.FirstName,removeMember.Designation);

            if (success)
            {
                ViewBag.Message = "Member removed successfully!";
                return RedirectToAction("Dashboard", "Company");
            }
            else
            {
                ViewBag.Message = "Member not found!";
            }

            return View();
        }

        public IActionResult ListMember(string search)
        {
            var getCompanyid = HttpContext.Session.GetString("CompanyId");

            if (string.IsNullOrEmpty(getCompanyid))
            {
                return RedirectToAction("Dashboard");
            }
            Guid companyId = new Guid(getCompanyid);

           
                var companyDetails = _userservice.GetMembersByCompanyId(companyId,search);
                if (companyDetails != null)
                {
                    return View(companyDetails);
                }
           
          
            return View();

        }

        public IActionResult ViewMember(Guid id)
        {
            var viewmember=_userservice.GetMemberById(id);
            if (viewmember == null)
            {
                return NotFound(); 
            }

            return View(viewmember);
        }

    }
}
